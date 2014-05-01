using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakCop
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) return;

            foreach (var item in source)
                action(item);
        }

        public static int RemoveAll<T>(this IList<T> source, IEnumerable<T> items)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (items == null) return 0;

            int count = 0;
            items.ForEach(i => 
                {
                    if (source.Remove(i)) count++;
                });
            
            return count;
        }
    }
}
