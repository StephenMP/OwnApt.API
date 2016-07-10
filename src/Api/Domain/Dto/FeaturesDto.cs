using OwnApt.Api.Extensions;

namespace OwnApt.Api.Domain.Dto
{
    public class FeaturesDto : Equatable<FeaturesDto>
    {
        #region Public Fields + Properties

        public AmmenityDto[] Ammentities { get; set; }
        public double Bathrooms { get; set; }
        public int Levels { get; set; }
        public ParkingDto Parking { get; set; }
        public int Rooms { get; set; }

        #endregion Public Fields + Properties

        #region Public Methods

        public override int GetHashCode()
        {
            return this.Rooms.GetHashCodeSafe()
                ^ this.Bathrooms.GetHashCodeSafe()
                ^ this.Ammentities.GetHashCodeSafe()
                ^ this.Levels.GetHashCodeSafe()
                ^ this.Parking.GetHashCodeSafe();
        }

        #endregion Public Methods
    }
}
