using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPersonService
    {
        #region Methods

        Task<PersonModel> CreateAsync(PersonModel personModel);

        Task DeleteAsync(string id);

        Task<PersonModel> ReadAsync(string id);

        Task UpdateAsync(PersonModel personModel);

        #endregion Methods
    }
}
