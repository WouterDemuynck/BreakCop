using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// Represents a change to a public field.
    /// </summary>
    [DebuggerDisplay("{ChangeType} ({Category}), {Compatibility}, Field = {FieldName}")]
    public class FieldChange : Change
    {
        private readonly FieldDefinition _field;

        internal FieldChange(ChangeType changeType, ChangeCategory category, CompatibilityLevel compatibility, FieldDefinition field)
            : base(changeType, category, compatibility)
        {
            if (field == null) throw new ArgumentNullException("field");
            _field = field;
        }

        /// <summary>
        /// Gets the full name of the type that has changed.
        /// </summary>
        public string FieldName
        {
            get
            {
                return _field.FullName;
            }
        }
    }
}
