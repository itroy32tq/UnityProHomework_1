using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    internal static class GenericExtension
    {
        public static IEnumerable<T> GetItemsOfType<T>(this IEnumerable<object> objects)
        { 
            return objects.OfType<T>();
        }
    }
}
