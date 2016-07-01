using System.Collections.Generic;

namespace Api.Domain.Model
{
	public class FeaturesModel
	{
		public int Rooms { get; set; }
		public double Bathrooms { get; set; }
		public IEnumerable<AmmentityModel> Ammentities { get; set; }
		public int Levels { get; set; }
		public ParkingModel Parking { get; set; }
	}
}