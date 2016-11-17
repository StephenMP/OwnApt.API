using System.Collections.Generic;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public class PropertyService : RepositoryService<PropertyModel, string, IPropertyRepository>, IPropertyService
    {
        #region Private Fields


        #endregion Private Fields

        #region Public Constructors

        public PropertyService(IPropertyRepository propertyRepository) : base(propertyRepository)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<PropertyModel>> ReadManyAsync(string[] propertyIds)
        {
            return await this.repository.ReadManyAsync(propertyIds);
        }

        #endregion Public Methods
    }
}
