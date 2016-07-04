using OwnApt.Api.Domain.Enum;

namespace OwnApt.Api.Repository.Entity
{
    public class ParkingEntity
    {
        public ParkingType Type { get; set; }
        public string Description { get; set; }
    }
}
