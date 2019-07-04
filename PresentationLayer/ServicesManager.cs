using BusinessLayer;
using PresentationLayer.Services;

namespace PresentationLayer
{
    public class ServicesManager
    {
        LoginManager _loginManager;
        private AccountService _accountService;

        public ServicesManager(LoginManager loginManager)
        {
            _loginManager = loginManager;
            _accountService = new AccountService(_loginManager);
        }
        public AccountService AccountService { get { return _accountService; } }
    }
}
