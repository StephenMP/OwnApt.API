using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class PropertyService : IPropertyService
    {
        #region Private Fields

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

        public async Task<PropertyModel[]> ReadManyAsync(string[] propertyIds)
        {
            return await this.propertyRepository.ReadManyAsync(propertyIds);
        }

        public async Task UpdateAsync(PropertyModel property)
        {
            await this.propertyRepository.UpdateAsync(property);
        }

        #endregion Public Methods
    }
}
