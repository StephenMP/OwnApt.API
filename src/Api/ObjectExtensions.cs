using System;
using System.Collections.Generic;
using System.Linq;

namespace OwnApt.Api
{
    public static class ObjectExtensions
    {
        #region Public Methods

        public static int GetHashCodeSafe(this object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.GetHashCode();
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
