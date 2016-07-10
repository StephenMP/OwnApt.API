using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Interface;
using System.Collections.Generic;
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

        public async Task<PropertyModel> CreateAsync(PropertyModel property)
        {
            return await this.propertyRepository.CreateAsync(property);
        }

        public async Task<PropertyModel> ReadAsync(string id)
        {
            return await this.propertyRepository.ReadAsync(id);
        }

        public async Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId)
        {
            return await this.propertyRepository.ReadPropertiesForOwnerAsync(ownerId);
        }

        public async Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId)
        {
            return await this.propertyRepository.ReadPropertiesForTenantAsync(tenantId);
        }

        public async Task UpdateAsync(PropertyModel property)
        {
            await this.propertyRepository.UpdateAsync(property);
        }

        public async Task DeleteAsync(string id)
        {
            await this.propertyRepository.DeleteAsync(id);
        }
    }
}