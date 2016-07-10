using System;

namespace OwnApt.Api
{
    public abstract class Equatable<T> : IEquatable<T>
    {
        #region Public Methods

        public bool Equals(T other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public abstract override int GetHashCode();

        #endregion Public Methods
    }
}
