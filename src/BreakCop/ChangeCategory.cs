using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// An enumeration indicating whether or not a change is breaking.
    /// </summary>
    public enum ChangeCategory
    {
        /// <summary>
        /// Indicates that the change was not evaluated, or no change was detected.
        /// </summary>
        None,
        /// <summary>
        /// Indicates that the change is non-breaking.
        /// </summary>
        NonBreaking,
        /// <summary>
        /// Indicates that the change is breaking, meaning either a binary-level break or
        /// a source-level break.
        /// </summary>
        Breaking
    }
}
