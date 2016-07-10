﻿using OwnApt.Api.Domain.Dto;
using OwnApt.Api.Domain.Enum;
using OwnApt.Api.Extensions;

namespace OwnApt.Api.Repository.Entity
{
    public class PropertyEntity : Equatable<PropertyEntity>
    {
        #region Public Fields + Properties

        public AddressDto Address { get; set; }
        public FeaturesDto Features { get; set; }
        public string Id { get; set; }
        public string[] OwnerIds { get; set; }
        public PropertyType PropertyType { get; set; }
        public string[] TenantIds { get; set; }

        #endregion Public Fields + Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Id.GetHashCodeSafe()
                ^ this.Address.GetHashCodeSafe()
                ^ this.OwnerIds.GetHashCodeSafe()
                ^ this.TenantIds.GetHashCodeSafe()
                ^ this.Features.GetHashCodeSafe()
                ^ this.PropertyType.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
