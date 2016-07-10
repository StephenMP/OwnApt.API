using OwnApt.Api.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPropertyService
    {
        Task<PropertyModel> CreateAsync(PropertyModel property);

        Task<PropertyModel> ReadAsync(string id);

        Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId);

        Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId);

        Task UpdateAsync(PropertyModel property);

        Task DeleteAsync(string id);
    }
}