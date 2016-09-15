using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class ParkingEntity : Equatable
    {
        #region Properties

        public string Description { get; set; }
        public string Type { get; set; }

        #endregion Properties
    }
}
