using OwnApt.Common.Enums;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class PropertyEntity : MongoEntity
    {
        #region Public Properties

        public AddressEntity Address { get; set; }
        public FeaturesEntity Features { get; set; }
        public string ImageUriString { get; set; }
        public string PropertyDescription { get; set; }
        public PropertyType PropertyType { get; set; }

        #endregion Public Properties
    }
}
