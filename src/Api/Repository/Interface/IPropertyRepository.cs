using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository : IRepository<PropertyModel>
    {
        #region Public Methods

        Task MapOwnerToPropertiesAsync(string ownerId, params string[] propertyIds);

        Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId);

        Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId);

        #endregion Public Methods
    }
}
