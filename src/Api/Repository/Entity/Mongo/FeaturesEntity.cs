using OwnApt.Common.Dto;
using System.Collections.Generic;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class FeaturesEntity : Equatable
    {
        #region Public Properties

        public IList<AmenityEntity> Amenities { get; set; }
        public double Bathrooms { get; set; }
        public int Levels { get; set; }
        public IList<ParkingEntity> Parking { get; set; }
        public int Rooms { get; set; }

        public int SqFootage { get; set; }

        #endregion Public Properties
    }
}
