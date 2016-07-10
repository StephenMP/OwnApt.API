using OwnApt.Api.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository : IRepository<PropertyModel>
    {
        Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId);

        Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId);
    }
}