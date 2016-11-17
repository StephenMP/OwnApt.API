using System.Collections.Generic;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository : IRepository<PropertyModel, string>
    {
        #region Public Methods

        Task<IEnumerable<PropertyModel>> ReadManyAsync(string[] propertyIds);

        #endregion Public Methods
    }
}
