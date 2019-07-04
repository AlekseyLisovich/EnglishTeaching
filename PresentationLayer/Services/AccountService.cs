using BusinessLayer;
using BusinessLayer.Interfaces;
using DataLayer.Entities.Account;
using PresentationLayer.Models.Account;
using System;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class AccountService
    {
        private LoginManager _loginManager;

        public AccountService(LoginManager loginManager)
        {
            this._loginManager = loginManager;
        }

        public async Task<User> RegisterUser(RegisterViewModel model)
        {
            User user = await _loginManager.UserRepository.GetByEmail(model.Email);

            if (user == null)
            {
                try
                {
                    user = new User
                    {
                        Email = model.Email,
                        Password = model.Password
                    };

                    Role userRole = await _loginManager.RoleRepository.GetByName("user");
                    if (userRole != null)
                        user.Role = userRole;

                    await _loginManager.UserRepository.AddAsync(user);

                    return user;
                }
                catch (Exception ex)
                {
                    //add logger
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public async Task<User> LoginUser(LoginViewModel model)
        {
            if (model != null)
            {
                try
                {
                    User user =  await _loginManager.UserRepository.GetUserWithCredentials(model.Email, model.Password);

                    if (user != null)
                        return user;
                }
                catch (Exception ex)
                {
                    //add logger
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }
    }
}
