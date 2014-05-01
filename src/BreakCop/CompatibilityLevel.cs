using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// An enumeration for indicating the compatibility level of a change.
    /// </summary>
    public enum CompatibilityLevel
    {
        /// <summary>
        /// Indicates that the change is neither binary nor source compatible with the older version, meaning
        /// that existing consumer assemblies will fail when using the newer version, as well as their source
        /// code cannot be recompiled against the newer version without changes.
        /// </summary>
        None,
        /// <summary>
        /// Indicates that the change is source compatible with the older version, meaning that while existing
        /// compiled consumer assemblies will fail when using the newer version, recompiling their source code
        /// will not fail.
        /// </summary>
        Source,
        /// <summary>
        /// Indicates that the change is binary compatible with the older version, meaning that existing compiled
        /// consumer assemblies will not fail when using the newer version.
        /// </summary>
        Binary
    }
}
