using BusinessLayer.Interfaces;
using BusinessLayer.Specifications;
using DataLayer.Entities.Account;
using EnglishTeaching.Models.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTeaching.Services
{
    public class AccountService
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<Role> _roleRepository;
        private readonly IAppLogger<User> _logger;

        public AccountService(IAsyncRepository<User> userRepository, IAsyncRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> RegisterUser(RegisterViewModel model)
        {
            _logger.LogInformation($"Registering the user");
            var userSpec = new UserWithItemsSpecification(model.Email);
            var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

            if (user == null)
            {
                try
                {
                    user = new User
                    {
                        Email = model.Email,
                        Password = model.Password
                    };

                    var roleSpec = new RoleWithEmailSpecification("user");
                    var userRole = (await _roleRepository.ListAsync(roleSpec)).FirstOrDefault();

                    if (userRole != null)
                        user.Role = userRole;

                    await _userRepository.AddAsync(user);

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
                    var userSpec = new UserWithItemsSpecification(model.Email, model.Password);
                    var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

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

        public async Task<UserProfileViewModel> GetUserProfile(string userName)
        {
            if (userName != null)
            {
                try
                {
                    var userSpec = new UserWithItemsSpecification(userName);
                    var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

                    if (user != null)
                    {
                        UserProfileViewModel profileModel = new UserProfileViewModel
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Name = user.Name,
                            Age = user.Age,
                            CellPhone = user.CellPhone,
                            Company = user.Company
                        };

                        return profileModel;
                    }
                }
                catch (Exception ex)
                {
                    //add logger
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }
        public async void SaveUserProfile(UserProfileViewModel model)
        {
            if (model != null)
            {
                try
                {
                    var userSpec = new UserWithItemsSpecification(model.Id);
                    var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

                    if (user != null)
                    {
                        user = new User
                        {
                            Id = model.Id,
                            Email = model.Email,
                            Password = user.Password,
                            Role = user.Role,
                            RoleId = user.RoleId,
                            Name = model.Name,
                            Age = model.Age,
                            CellPhone = model.CellPhone,
                            Company = model.Company
                        };

                        await _userRepository.UpdateAsync(user);
                    }
                }
                catch (Exception ex)
                {
                    //add logger
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
