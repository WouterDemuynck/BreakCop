using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakCop
{
    /// <summary>
    /// Provides methods for comparing two versions of an assembly and detecting public changes
    /// between them.
    /// </summary>
    public class AssemblyComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyComparer"/> class.
        /// </summary>
        public AssemblyComparer()
        {
        }

        /// <summary>
        /// Compares two assemblies and returns the public changes between them.
        /// </summary>
        /// <param name="sourcePath">
        /// The file system location of the source assembly, which should be the
        /// older version of the assembly.
        /// </param>
        /// <param name="targetPath">
        /// The file system location of the target assembly, which should be the
        /// newer version of the assembly.
        /// </param>
        /// <returns>
        /// A collection of changes between the specified assemblies.
        /// </returns>
        public IEnumerable<Change> Compare(string sourcePath, string targetPath)
        {
            AssemblyDefinition source = AssemblyDefinition.ReadAssembly(sourcePath);
            AssemblyDefinition target = AssemblyDefinition.ReadAssembly(targetPath);

            return Compare(source, target);
        }

        private IEnumerable<Change> Compare(AssemblyDefinition source, AssemblyDefinition target)
        {
            var sourceTypes = GetPublicTypes(source);
            var targetTypes = GetPublicTypes(target);

            // First detect all new types in the newer assembly.
            var addedTypes = targetTypes.Except(sourceTypes, CecilComparer.TypeByFullName).ToList();
            foreach (var addedType in addedTypes)
                yield return new TypeChange(ChangeType.TypeAdded, ChangeCategory.NonBreaking, CompatibilityLevel.Binary, addedType);
            targetTypes.RemoveAll(addedTypes);

            // Now detect all removed types in the newer assembly.
            var removedTypes = sourceTypes.Except(targetTypes, CecilComparer.TypeByFullName).ToList();
            foreach (var removedType in removedTypes)
                yield return new TypeChange(ChangeType.TypeRemoved, ChangeCategory.Breaking, CompatibilityLevel.None, removedType);
            sourceTypes.RemoveAll(removedTypes);

            // We have now reduced the input to the types that occur in both assemblies, now we will need
            // to compare those one by one...
            foreach (var targetType in targetTypes)
            {
                var sourceType = sourceTypes.Single(t => CecilComparer.TypeByFullName.Equals(t, targetType));
                TypeComparer comparer = new TypeComparer();
                foreach (var change in comparer.Compare(sourceType, targetType))
                    yield return change;
            }
        }

        private IList<TypeDefinition> GetPublicTypes(AssemblyDefinition target)
        {
            return target
                .Modules
                .SelectMany(m => m.Types)
                .Where(t => t.IsPublic)
                .OrderBy(t => t.FullName)
                .ToList();
        }
    }
}
