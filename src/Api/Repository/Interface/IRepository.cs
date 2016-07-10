using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRepository<TModel>
    {
        Task<TModel> CreateAsync(TModel model);

        Task<TModel> ReadAsync(string id);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(string id);
    }
}