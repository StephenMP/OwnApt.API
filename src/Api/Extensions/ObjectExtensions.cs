using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Extensions
{
    public static class ObjectExtensions
    {
        #region Public Methods

        public static int GetHashCodeSafe(this object obj)
        {
            return obj == null ? 0 : obj.GetHashCode();
        }

        public static int GetHashCodeSafe<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null || enumerable.Count() == 0)
            {
                return 0;
            }

            var hash = 0;

            foreach (var element in enumerable)
            {
                hash ^= element.GetHashCodeSafe();
            }

            return hash;
        }

        #endregion Public Methods
    }
}
