﻿using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRepository<TModel>
    {
        #region Public Methods

        Task<TModel> CreateAsync(TModel model);

        Task DeleteAsync(string id);

        Task<TModel> ReadAsync(string id);

        Task UpdateAsync(TModel model);

        #endregion Public Methods
    }
}
