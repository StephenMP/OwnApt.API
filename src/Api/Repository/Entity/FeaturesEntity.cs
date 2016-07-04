using System.Collections.Generic;

namespace OwnApt.Api.Repository.Entity
{
	public class FeaturesEntity
	{
		public int Rooms { get; set; }
		public double Bathrooms { get; set; }
		public IEnumerable<AmmentityEntity> Ammentities { get; set; }
		public int Levels { get; set; }
		public ParkingEntity Parking { get; set; }
	}
}