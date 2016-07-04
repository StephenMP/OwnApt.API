using OwnApt.Api.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPropertyService
    {
        Task CreatePropertyAsync(PropertyModel property);
        Task<PropertyModel> ReadPropertyAsync(string id);
        Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId);
        Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId);
        Task UpdatePropertyAsync(PropertyModel property);
        Task DeletePropertyAsync(string id);
    }
}