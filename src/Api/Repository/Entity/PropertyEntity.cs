using OwnApt.Api.Contract.Dto;
using OwnApt.Api.Extensions;
using OwnApt.Common.Dto;
using OwnApt.Common.Enum;
using System.Collections.Generic;

namespace OwnApt.Api.Repository.Entity
{
    public class PropertyEntity : Equatable<PropertyEntity>
    {
        #region Properties

        public AddressDto Address { get; set; }
        public FeaturesDto Features { get; set; }
        public string Id { get; set; }
        public List<string> OwnerIds { get; set; }
        public PropertyType PropertyType { get; set; }
        public List<string> TenantIds { get; set; }

        #endregion Properties

        #region Methods

        public override int GetHashCode()
        {
            return this.Id.GetHashCodeSafe()
                ^ this.Address.GetHashCodeSafe()
                ^ this.OwnerIds.GetHashCodeSafe()
                ^ this.TenantIds.GetHashCodeSafe()
                ^ this.Features.GetHashCodeSafe()
                ^ this.PropertyType.GetHashCodeSafe();
        }

        #endregion Methods
    }
}
