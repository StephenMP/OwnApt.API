using System;

namespace OwnApt.Api.Domain.Dto
{
    public abstract class Equatable<TDto> : IEquatable<TDto>
    {
        #region Public Methods

        public bool Equals(TDto other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public abstract override int GetHashCode();

        #endregion Public Methods
    }
}
