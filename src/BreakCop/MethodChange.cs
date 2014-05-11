using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// Represents a change to a public method.
    /// </summary>
    [DebuggerDisplay("{ChangeType} ({Category}), {Compatibility}, Method = {MethodName}")]
    public class MethodChange : Change
    {
        private readonly MethodDefinition _method;

        internal MethodChange(ChangeType changeType, ChangeCategory category, CompatibilityLevel compatibility, MethodDefinition method)
            : base(changeType, category, compatibility)
        {
            if (method == null) throw new ArgumentNullException("method");
            _method = method;
        }

        /// <summary>
        /// Gets the full name and signature of the changed method.
        /// </summary>
        public string MethodName
        {
            get
            {
                return _method.FullName;
            }
        }

        public override string ToString()
        {
            string format = null;
            switch (ChangeType)
            {
                case BreakCop.ChangeType.MethodAdded:
                    format = "Method {0} has been added.";
                    break;

                case BreakCop.ChangeType.MethodRemoved:
                    format = "Method {0} has been removed.";
                    break;
            }

            if (format == null) return base.ToString();
            return string.Format(CultureInfo.InvariantCulture, format, MethodName);
        }
    }
}
