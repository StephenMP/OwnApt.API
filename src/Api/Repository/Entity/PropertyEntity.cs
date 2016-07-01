using Api.Domain.Enum;
using System.Collections.Generic;

namespace Api.Repository.Entity
{
    public class PropertyEntity
	{
		public string Id { get; set; }
		public AddressEntity Address { get; set; }
		public IEnumerable<PersonEntity> Owners { get; set; }
		public IEnumerable<PersonEntity> Tenants { get; set; }
		public IEnumerable<FeaturesEntity> Features { get; set; }
		public PropertyType PropertyType { get; set; }
	}
}
