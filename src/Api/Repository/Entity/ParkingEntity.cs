using Api.Domain.Enum;

namespace Api.Repository.Entity
{
    public class ParkingEntity
    {
        public ParkingType Type { get; set; }
        public string Description { get; set; }
    }
}
