using OwnApt.Api.Domain.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPersonService
    {
        Task<PersonModel> CreateAsync(PersonModel personModel);

        Task<PersonModel> ReadAsync(string id);

        Task UpdateAsync(PersonModel personModel);

        Task DeleteAsync(string id);
    }
}