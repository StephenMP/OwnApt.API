using OwnApt.Api.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRegisteredTokenRepository : IRepository<RegisteredTokenModel, RegisteredTokenModel, int>
    {
        Task<RegisteredTokenModel> ReadByTokenAsync(string token);
    }
}
