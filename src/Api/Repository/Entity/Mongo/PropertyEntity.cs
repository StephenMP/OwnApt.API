using OwnApt.Common.Dto;
using OwnApt.Common.Enum;
using System.Collections.Generic;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class PropertyEntity : Equatable
    {
        #region Properties

        public AddressEntity Address { get; set; }
        public FeaturesEntity Features { get; set; }
        public string Id { get; set; }
        public IList<string> OwnerIds { get; set; }
        public PropertyType PropertyType { get; set; }
        public IList<string> TenantIds { get; set; }

        #endregion Properties
    }
}
