﻿using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IPropertyService
    {
        #region Public Methods

        Task<PropertyModel> CreateAsync(PropertyModel property);

        Task DeleteAsync(string id);

        Task<PropertyModel> ReadAsync(string id);

        Task<PropertyModel[]> ReadManyAsync(string[] propertyIds);

        Task UpdateAsync(PropertyModel property);

        #endregion Public Methods
    }
}
