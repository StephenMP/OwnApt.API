using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IPropertyRepository : IRepository<PropertyModel, PropertyModel, string>
    {
        #region Public Methods

        Task<PropertyModel[]> ReadManyAsync(string[] propertyIds);

        #endregion Public Methods
    }
}
