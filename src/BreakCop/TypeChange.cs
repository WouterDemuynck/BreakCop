using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// Represents a change to a public type.
    /// </summary>
    [DebuggerDisplay("{ChangeType} ({Category}), {Compatibility}, Type = {TypeName}")]
    public class TypeChange : Change
    {
        private readonly TypeDefinition _type;

        internal TypeChange(ChangeType changeType, ChangeCategory category, CompatibilityLevel compatibility, TypeDefinition type)
            : base(changeType, category, compatibility)
        {
            if (type == null) throw new ArgumentNullException("type");
            _type = type;
        }

        /// <summary>
        /// Gets the full name of the type that has changed.
        /// </summary>
        public string TypeName
        {
            get
            {
                return _type.FullName;
            }
        }
    }
}
