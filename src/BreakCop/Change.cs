using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakCop
{
    /// <summary>
    /// Represents a change detected when comparing two assemblies.
    /// </summary>
    [DebuggerDisplay("{ChangeType} ({Category}), {Compatibility}")]
    public abstract class Change
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeType"/> class using the specified
        /// change <paramref name="category"/> and <paramref name="compatibility"/> level.
        /// </summary>
        /// <param name="changeType">
        /// The <see cref="ChangeType"/> indicating the type of the change.
        /// </param>
        /// <param name="category">
        /// The <see cref="ChangeCategory"/> indicating whether or not this is a breaking change.
        /// </param>
        /// <param name="compatibility">
        /// The <see cref="CompatibilityLevel"/> indicating whether this change breaks binary or 
        /// source compatibility.
        /// </param>
        protected Change(ChangeType changeType, ChangeCategory category, CompatibilityLevel compatibility)
        {
            ChangeType = changeType;
            Category = category;
            Compatibility = compatibility;
        }

        /// <summary>
        /// Gets a value indicating the type of the change
        /// </summary>
        public ChangeType ChangeType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether or not this is a breaking change.
        /// </summary>
        public ChangeCategory Category
        {
            get;
            private set;
        }

        /// <summary>
        /// Get a value indicating the compatibility level of the change.
        /// </summary>
        public CompatibilityLevel Compatibility
        {
            get;
            private set;
        }
    }
}
