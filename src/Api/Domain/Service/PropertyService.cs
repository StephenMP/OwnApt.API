using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class PropertyService : IPropertyService
    {
        #region Private Fields

        public async Task MapOwnerToPropertiesAsync(MapOwnerToPropertiesDto mapOwnerToPropertiesDto)
        {
            await this.propertyRepository.MapOwnerToPropertiesAsync(mapOwnerToPropertiesDto.OwnerId, mapOwnerToPropertiesDto.PropertyIds);
        }


        private readonly IPropertyRepository propertyRepository;

        #endregion Private Fields

        #region Public Constructors

        public PropertyService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PropertyModel> CreateAsync(PropertyModel property)
        {
            return await this.propertyRepository.CreateAsync(property);
        }

        public async Task DeleteAsync(string id)
        {
            await this.propertyRepository.DeleteAsync(id);
        }

        public async Task<PropertyModel> ReadAsync(string id)
        {
            return await this.propertyRepository.ReadAsync(id);
        }

        public async Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId)
        {
            return await this.propertyRepository.ReadPropertiesForOwnerAsync(ownerId);
        }

        public async Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId)
        {
            return await this.propertyRepository.ReadPropertiesForTenantAsync(tenantId);
        }

        public async Task UpdateAsync(PropertyModel property)
        {
            await this.propertyRepository.UpdateAsync(property);
        }

        #endregion Public Methods
    }
}
