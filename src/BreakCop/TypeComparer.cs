using Mono.Cecil;
using Mono.Cecil.Pdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakCop
{
    internal class TypeComparer
    {
        public TypeComparer()
        {
        }

        public IEnumerable<Change> Compare(TypeDefinition source, TypeDefinition target)
        {
            // Sealing a previously unsealed class is a breaking change...            
            if (!source.IsSealed && target.IsSealed)
                yield return new TypeChange(ChangeType.TypeSealed, ChangeCategory.Breaking, CompatibilityLevel.None, target);

            if (source.IsSealed && !target.IsSealed)
                yield return new TypeChange(ChangeType.TypeUnsealed, ChangeCategory.NonBreaking, CompatibilityLevel.Binary, target);

            // Making a previously concrete class abstract is a breaking change...
            if (!source.IsAbstract && target.IsAbstract)
                yield return new TypeChange(ChangeType.TypeAbstracted, ChangeCategory.Breaking, CompatibilityLevel.None, target);

            if (source.IsAbstract && !target.IsAbstract)
                yield return new TypeChange(ChangeType.TypeUnabstracted, ChangeCategory.NonBreaking, CompatibilityLevel.Binary, target);

            foreach (var change in CompareMethods(source, target))
                yield return change;

            foreach (var change in CompareFields(source, target))
                yield return change;
        }

        private IEnumerable<Change> CompareFields(TypeDefinition source, TypeDefinition target)
        {
            var targetFields = target.Fields.Where(f => f.IsFamily || f.IsPublic || f.IsFamilyOrAssembly).ToList();
            var sourceFields = source.Fields.Where(f => f.IsFamily || f.IsPublic || f.IsFamilyOrAssembly).ToList();

            // First detect all new types in the newer assembly.
            var addedFields = targetFields.Except(sourceFields, CecilComparer.FieldByFullName).ToList();
            foreach (var addedField in addedFields)
                yield return new FieldChange(ChangeType.FieldAdded, ChangeCategory.NonBreaking, CompatibilityLevel.Binary, addedField);
            targetFields.RemoveAll(addedFields);

            // Now detect all removed types in the newer assembly.
            var removedFields = sourceFields.Except(targetFields, CecilComparer.FieldByFullName).ToList();
            foreach (var removedType in removedFields)
                yield return new FieldChange(ChangeType.FieldRemoved, ChangeCategory.Breaking, CompatibilityLevel.None, removedType);
            sourceFields.RemoveAll(removedFields);
        }

        private IEnumerable<Change> CompareMethods(TypeDefinition source, TypeDefinition target)
        {
            var targetMethods = target.Methods.Where(m => m.IsFamily || m.IsPublic || m.IsFamilyOrAssembly).ToList();
            var sourceMethods = source.Methods.Where(m => m.IsFamily || m.IsPublic || m.IsFamilyOrAssembly).ToList();

            // Check for removed and changed methods...
            foreach (var sourceMethod in sourceMethods)
            {
                // Get the list of overloads of this method in the target type...
                var currentTargetMethods = targetMethods.Where(m => CecilComparer.MethodByName.Equals(m, sourceMethod)).ToList();

                if (currentTargetMethods == null || !currentTargetMethods.Any())
                {
                    // We don't have any public methods by that name...
                    yield return new MethodChange(ChangeType.MethodRemoved, ChangeCategory.Breaking, CompatibilityLevel.None, sourceMethod);
                }
                else if (!currentTargetMethods.Contains(sourceMethod, CecilComparer.MethodByFullName))
                {
                    // The signature of the method has changed without having a backward compatible overload...
                    yield return new MethodChangedChange(ChangeCategory.Breaking, CompatibilityLevel.None, sourceMethod, currentTargetMethods);

                    // We don't want to report additions on these methods...
                    targetMethods.RemoveAll(currentTargetMethods);
                }
            }

            // Check for added methods...
            foreach (var targetMethod in targetMethods)
            {
                var currentSourceMethods = sourceMethods.Where(m => CecilComparer.MethodByName.Equals(m, targetMethod)).ToList();

                if (currentSourceMethods == null || !currentSourceMethods.Any() || !currentSourceMethods.Contains(targetMethod, CecilComparer.MethodByFullName))
                {
                    yield return new MethodChange(ChangeType.MethodAdded, ChangeCategory.NonBreaking, CompatibilityLevel.Binary, targetMethod);
                }
            }
        }
    }
}
