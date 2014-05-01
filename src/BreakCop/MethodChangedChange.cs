using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// Represents a change to a public method's signature.
    /// </summary>
    [DebuggerDisplay("{ChangeType} ({Category}), {Compatibility}, Method = {MethodName}{OverloadsString}")]
    public class MethodChangedChange : MethodChange
    {
        private readonly IEnumerable<MethodDefinition> _overloads;
        private string _overloadsString;

        internal MethodChangedChange(ChangeCategory category, CompatibilityLevel compatibility, MethodDefinition method, IEnumerable<MethodDefinition> overloads)
            : base(ChangeType.MethodChanged, category, compatibility, method)
        {
            _overloads = overloads;
        }

        /// <summary>
        /// Gets the collection of possible overloads of the method in the newer version of the type.
        /// </summary>
        public IEnumerable<string> Overloads
        {
            get
            {
                return _overloads == null ? Enumerable.Empty<string>() : _overloads.Select(m => m.FullName);
            }
        }

        private string OverloadsString
        {
            get
            {
                if (_overloadsString != null) return _overloadsString;

                if (_overloads == null) return string.Empty;
                var overloads = _overloads.ToList();
                _overloadsString = overloads.Count > 0 
                    ? string.Format(", Overloads = [{0}]", string.Join(", ", overloads.Select(m => m.FullName)))
                    : string.Empty;

                return _overloadsString;
            }
        }
    }
}
