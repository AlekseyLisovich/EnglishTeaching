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
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> RegisterUser(RegisterViewModel model)
        {
            User user = await _userRepository.GetByEmail(model.Email);

            if (user == null)
            {
                try
                {
                    user = new User
                    {
                        Email = model.Email,
                        Password = model.Password
                    };

                    Role userRole = await _roleRepository.GetByName("user");
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
                    User user = await _userRepository.GetUserWithCredentials(model.Email, model.Password);

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
                    User user = await _userRepository.GetByIdAsync(model.Id);

                    if (user != null)
                    {
                        user = new User
                        {
                            Id = model.Id,
                            Email = model.Email,
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
