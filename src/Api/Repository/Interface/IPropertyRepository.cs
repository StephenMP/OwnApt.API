using OwnApt.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository
    {
        void CreateProperty(PropertyModel property);
        Task CreatePropertyAsync(PropertyModel property);

        PropertyModel ReadProperty(string id);
        Task<PropertyModel> ReadPropertyAsync(string id);

        IEnumerable<PropertyModel> ReadPropertiesForOwner(string ownerId);
        Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId);

        IEnumerable<PropertyModel> ReadPropertiesForTenant(string tenantId);
        Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId);

        void UpdateProperty(PropertyModel property);
        Task UpdatePropertyAsync(PropertyModel property);

        void DeleteProperty(string id);
        Task DeletePropertyAsync(string id);
    }
}
