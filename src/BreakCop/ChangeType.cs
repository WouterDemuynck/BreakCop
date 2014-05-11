using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakCop
{
    /// <summary>
    /// An enumeration indicating the type of a change.
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// Indicates an unknown change.
        /// </summary>
        Unknown,
        /// <summary>
        /// Indicates that a new type was introduced.
        /// </summary>
        TypeAdded,
        /// <summary>
        /// Indicates that a public type was removed.
        /// </summary>
        TypeRemoved,
        /// <summary>
        /// Indicates that a previously unsealed type has been sealed.
        /// </summary>
        TypeSealed,
        /// <summary>
        /// Indicates that a previously sealed type has been unsealed.
        /// </summary>
        TypeUnsealed,
        /// <summary>
        /// Indicates that a previously concrete class has been abstracted.
        /// </summary>
        TypeAbstracted,
        /// <summary>
        /// Indicates that a previously abstract class has been unabstracted.
        /// </summary>
        TypeUnabstracted,
        /// <summary>
        /// Indicates that a new method was added.
        /// </summary>
        MethodAdded,
        /// <summary>
        /// Indicates that a public method has been removed.
        /// </summary>
        MethodRemoved,
        /// <summary>
        /// Indicates that the signature of an existing method has changed.
        /// </summary>
        MethodChanged,
        /// <summary>
        /// Indicates that a new public field was added.
        /// </summary>
        FieldAdded,
        /// <summary>
        /// Indicates that a public field has been removed.
        /// </summary>
        FieldRemoved,
    }
}
