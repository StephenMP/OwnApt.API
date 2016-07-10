using OwnApt.Api.Domain.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository : IRepository<PropertyModel>
    {
        #region Public Methods

        Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId);

        Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId);

        #endregion Public Methods
    }
}
