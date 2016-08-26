using OwnApt.Api.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
