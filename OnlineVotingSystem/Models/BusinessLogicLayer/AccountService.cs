using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.BusinessLogicLayer
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Register(RegisterViewModel viewModel)
        {
            User user = new User() { Username = viewModel.Username, Password = viewModel.Password};
           await _accountRepository.CreateAccount(user);
        }


        public async Task<bool> Login(LoginViewModel viewModel)
        {
            User user = new User() { Username = viewModel.Username, Password = viewModel.Password };
            return await _accountRepository.VerifyAccount(user);
        }

        public async Task<bool> IsAdmin(string username)
        {
            return await _accountRepository.IsAdmin(username);
        }

        public async Task<string> GetUserHighestRoleName(string username)
        {
            return await _accountRepository.GetUserHighestRoleName(username);
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");
            return await _accountRepository.IsUsernameExist(username.Trim());
        }

    }
}
