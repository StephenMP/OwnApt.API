using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class AmenityEntity : Equatable
    {
        #region Public Properties

        public string Description { get; set; }
        public string Type { get; set; }

        #endregion Public Properties
    }
}
