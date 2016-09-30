using OwnApt.Common.Dto;
using OwnApt.Common.Enum;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class PropertyEntity : Equatable
    {
        #region Public Properties

        public AddressEntity Address { get; set; }
        public FeaturesEntity Features { get; set; }
        public string Id { get; set; }
        public string ImageUriString { get; set; }
        public string PropertyDescription { get; set; }
        public PropertyType PropertyType { get; set; }

        #endregion Public Properties
    }
}
