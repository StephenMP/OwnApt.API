using Microsoft.AspNetCore.Identity;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Interface;
using OwnApt.Authentication.Client.Security;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class UserLoginService : IUserLoginService
    {
        #region Private Fields

        private IUserLoginRepository userLoginRepository;

        #endregion Private Fields

        #region Public Constructors

        public UserLoginService(IUserLoginRepository userLoginRepository)
        {
            this.userLoginRepository = userLoginRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> CreateAsync(UserLoginModel suppliedModel)
        {
            var existingUserModel = await this.ReadByEmailAsync(suppliedModel.Email);

            if (existingUserModel != null)
            {
                return await Task.FromResult(false);
            }

            var userIdentity = await this.BuildIdentity(suppliedModel.Email);
            var passwordHasher = await this.BuildPasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(userIdentity, CryptoProvider.Decrypt(suppliedModel.Password));

            var newUserLoginModel = new UserLoginModel
            {
                UserId = Guid.NewGuid().ToString("N"),
                Email = suppliedModel.Email,
                Password = hashedPassword
            };

            var createdModel = await this.userLoginRepository.CreateAsync(newUserLoginModel);
            var created = createdModel != null;

            return await Task.FromResult(created);
        }

        public async Task DeleteAsync(string id)
        {
            await this.userLoginRepository.DeleteAsync(id);
        }

        public async Task<UserLoginModel> ReadAsync(string id)
        {
            return await this.userLoginRepository.ReadAsync(id);
        }

        public async Task<UserLoginModel> ReadByEmailAsync(string email)
        {
            return await this.userLoginRepository.ReadByEmailAsync(email);
        }

        public async Task<UserLoginModel> RehashUserPassword(UserLoginModel suppliedModel)
        {
            var loginModel = await this.ReadByEmailAsync(suppliedModel.Email);
            var userIdentity = await this.BuildIdentity(suppliedModel.Email);
            var passwordHasher = await this.BuildPasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(userIdentity, CryptoProvider.Decrypt(suppliedModel.Password));

            var newUserLoginModel = new UserLoginModel
            {
                UserId = loginModel.UserId,
                Email = loginModel.Email,
                Password = hashedPassword
            };

            await this.UpdateAsync(newUserLoginModel);

            newUserLoginModel.Password = CryptoProvider.Encrypt(newUserLoginModel.Password);
            return await Task.FromResult(newUserLoginModel);
        }

        public async Task UpdateAsync(UserLoginModel model)
        {
            await this.userLoginRepository.UpdateAsync(model);
        }

        public async Task<UserLoginModel> VerifyUser(UserLoginModel suppliedModel)
        {
            var loginModel = await this.ReadByEmailAsync(suppliedModel.Email);

            if (loginModel == null)
            {
                var failedLoginModel = new UserLoginModel { VerificationResult = PasswordVerificationResult.Failed };
                return await Task.FromResult(failedLoginModel);
            }

            var suppliedEmail = suppliedModel.Email;
            var suppliedPassword = CryptoProvider.Decrypt(suppliedModel.Password);

            var storedEmail = loginModel.Email;
            var storedHashedPassword = loginModel.Password;

            var userIdentity = await this.BuildIdentity(suppliedModel.Email);
            var passwordHasher = await this.BuildPasswordHasher();
            var verificationResult = passwordHasher.VerifyHashedPassword(userIdentity, storedHashedPassword, suppliedPassword);

            loginModel.VerificationResult = verificationResult;
            loginModel.Password = String.Empty;
            return await Task.FromResult(loginModel);
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<GenericIdentity> BuildIdentity(string email)
        {
            return await Task.FromResult(new GenericIdentity(email));
        }

        private async Task<PasswordHasher<GenericIdentity>> BuildPasswordHasher()
        {
            return await Task.FromResult(new PasswordHasher<GenericIdentity>());
        }

        #endregion Private Methods
    }
}
