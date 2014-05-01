using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakCop
{
    internal class CallbackEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equals;
        private readonly Func<T, int> _getHashCode;

        public CallbackEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            if (equals == null) throw new ArgumentNullException("equals");
            if (getHashCode == null) getHashCode = (T obj) => obj.GetHashCode();

            _equals = equals;
            _getHashCode = getHashCode;
        }

        public bool Equals(T x, T y)
        {
            if ((x == null && y != null) || (x != null && y == null)) return false;
            if (x == null && y == null) return true;
            return _equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            if (obj == null) return int.MinValue;
            return _getHashCode(obj);
        }
    }
}
