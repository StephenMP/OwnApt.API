using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository : IRepository<PropertyModel>
    {
        #region Methods

        Task MapOwnerToPropertiesAsync(string ownerId, params string[] propertyIds);

        Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId);

        Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId);

        #endregion Methods
    }
}
