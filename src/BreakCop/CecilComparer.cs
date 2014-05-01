using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakCop
{
    internal static class CecilComparer
    {
        public static readonly IEqualityComparer<TypeDefinition> TypeByFullName =
            new CallbackEqualityComparer<TypeDefinition>((x, y) => x.FullName.Equals(y.FullName), obj => obj.FullName.GetHashCode());

        public static readonly IEqualityComparer<MethodDefinition> MethodByFullName =
            new CallbackEqualityComparer<MethodDefinition>((x, y) => x.FullName.Equals(y.FullName), obj => obj.FullName.GetHashCode());

        public static readonly IEqualityComparer<MethodDefinition> MethodByName =
            new CallbackEqualityComparer<MethodDefinition>((x, y) => x.Name.Equals(y.Name), obj => obj.Name.GetHashCode());
    }
}
