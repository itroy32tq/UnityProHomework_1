using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    internal static class GenericExtension
    {
        public static T GetUniqueItemOfType<T>(this IEnumerable<object> objects)
        { 
            return objects.OfType<T>().FirstOrDefault();
        }
    }
}
