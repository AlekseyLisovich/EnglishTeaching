using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    public class LoginManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public LoginManager(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public IUserRepository UserRepository { get { return _userRepository; } }
        public IRoleRepository RoleRepository { get { return _roleRepository; } }
    }
}
