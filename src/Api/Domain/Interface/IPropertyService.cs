using System.Collections.Generic;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPropertyService
    {
        #region Public Methods

        Task<PropertyModel> CreateAsync(PropertyModel property);

        Task DeleteAsync(string id);

        Task<IEnumerable<PropertyModel>> ReadAllAsync();

        Task<PropertyModel> ReadAsync(string id);

        Task<IEnumerable<PropertyModel>> ReadManyAsync(string[] propertyIds);

        Task UpdateAsync(PropertyModel property);

        #endregion Public Methods
    }
}
