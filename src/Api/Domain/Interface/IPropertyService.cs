using OwnApt.Api.Contract.Dto;
using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPropertyService
    {
        #region Public Methods

        Task<PropertyModel> CreateAsync(PropertyModel property);

        Task DeleteAsync(string id);

        Task MapOwnerToPropertiesAsync(MapOwnerToPropertiesDto mapOwnerToPropertiesDto);

        Task<PropertyModel> ReadAsync(string id);

        Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId);

        Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId);

        Task UpdateAsync(PropertyModel property);

        #endregion Public Methods
    }
}
