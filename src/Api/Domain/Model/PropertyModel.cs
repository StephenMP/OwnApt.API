using Api.Domain.Enum;
using System.Collections.Generic;

namespace Api.Domain.Model
{
    public class PropertyModel
	{
		public string Id { get; set; }
		public AddressModel Address { get; set; }
		public IEnumerable<PersonModel> Owners { get; set; }
		public IEnumerable<PersonModel> Tenants { get; set; }
		public IEnumerable<FeaturesModel> Features { get; set; }
		public PropertyType PropertyType { get; set; }
	}
}
