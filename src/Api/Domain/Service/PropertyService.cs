using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task CreatePropertyAsync(PropertyModel property)
        {
            await this.propertyRepository.CreatePropertyAsync(property);
        }

        public async Task<PropertyModel> ReadPropertyAsync(string id)
        {
            return await this.propertyRepository.ReadPropertyAsync(id);
        }

        public async Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId)
        {
            return await this.propertyRepository.ReadPropertiesForOwnerAsync(ownerId);
        }

        public async Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId)
        {
            return await this.propertyRepository.ReadPropertiesForTenantAsync(tenantId);
        }

        public async Task UpdatePropertyAsync(PropertyModel property)
        {
            await this.propertyRepository.UpdatePropertyAsync(property);
        }

        public async Task DeletePropertyAsync(string id)
        {
            await this.propertyRepository.DeletePropertyAsync(id);
        }
    }
}
