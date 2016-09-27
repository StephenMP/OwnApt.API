using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OwnApt.Api.Repository.Sql.Core
{
    public class SqlRegisteredTokenRepository : IRegisteredTokenRepository
    {
        readonly CoreContext context;
        readonly IMapper mapper;

        public SqlRegisteredTokenRepository(CoreContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<RegisteredTokenModel> CreateAsync(RegisteredTokenModel model)
        {
            var entity = this.mapper.Map<RegisteredTokenEntity>(model);
            this.context.RegisteredToken.Add(entity);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<RegisteredTokenModel>(entity);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotSupportedException("We do not delete registerd tokens");
        }

        public async Task<RegisteredTokenModel> ReadAsync(int id)
        {
            var entity = await this.context
                                   .RegisteredToken
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(e => e.TokenId == id);

            var model = this.mapper.Map<RegisteredTokenModel>(entity);
            return model;
        }

        public async Task<RegisteredTokenModel> ReadByTokenAsync(string token)
        {
            var entity = await this.context
                                   .RegisteredToken
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(e => e.Token == token);

            var model = this.mapper.Map<RegisteredTokenModel>(entity);
            return model;
        }

        public Task UpdateAsync(RegisteredTokenModel model)
        {
            throw new NotSupportedException("We do no update registered tokens");
        }
    }
}
