using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Sql
{
    public class UserLoginRepository : IUserLoginRepository
    {
        #region Private Fields

        private CoreContext coreContext;
        private IMapper mapper;

        #endregion Private Fields

        #region Public Constructors

        public UserLoginRepository(CoreContext coreContext, IMapper mapper)
        {
            this.coreContext = coreContext;
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UserLoginModel> CreateAsync(UserLoginModel model)
        {
            //using (var context = this.coreContext)
            //{
            var entity = this.mapper.Map<UserLoginEntity>(model);
            this.coreContext.UserLogin.Add(entity);
            await this.coreContext.SaveChangesAsync();
            //}

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            //using (var context = this.coreContext)
            //{
            var entity = await this.coreContext.UserLogin.SingleOrDefaultAsync(e => e.UserId == id);
            this.coreContext.UserLogin.Remove(entity);
            await this.coreContext.SaveChangesAsync();
            //}
        }

        public async Task<UserLoginModel> ReadAsync(string id)
        {
            //using (var context = this.coreContext)
            //{
            var entity = await this.coreContext.UserLogin.SingleOrDefaultAsync(e => e.UserId == id);
            var model = this.mapper.Map<UserLoginModel>(entity);
            return model;
            //}
        }

        public async Task<UserLoginModel> ReadByEmailAsync(string email)
        {
            //using (var context = this.coreContext)
            //{
            var entity = await this.coreContext.UserLogin.SingleOrDefaultAsync(e => e.Email == email);
            var model = this.mapper.Map<UserLoginModel>(entity);
            return model;
            //}
        }

        public async Task UpdateAsync(UserLoginModel model)
        {
            //using (var context = this.coreContext)
            //{
            var entity = await this.coreContext.UserLogin.SingleOrDefaultAsync(e => e.UserId == model.UserId);
            if (entity != null)
            {
                entity.Email = model.Email;
                entity.Password = model.Password;
                await this.coreContext.SaveChangesAsync();
            }
            //}
        }

        #endregion Public Methods
    }
}
