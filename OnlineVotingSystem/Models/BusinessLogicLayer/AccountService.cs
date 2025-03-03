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
            if (string.IsNullOrEmpty(viewModel.Username)) throw new Exception("The username cannot be null or empty string.");
            else if (string.IsNullOrEmpty(viewModel.Password)) throw new Exception("The password cannot be null or empty string.");

            User user = new User() { Username = viewModel.Username.Trim(), Password = viewModel.Password };
            await _accountRepository.CreateAccount(user);
        }

        public async Task<bool> Login(LoginViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Username)) throw new Exception("The username cannot be null or empty string.");
            else if (string.IsNullOrEmpty(viewModel.Password)) throw new Exception("The password cannot be null or empty string.");

            User user = new User() { Username = viewModel.Username.Trim(), Password = viewModel.Password };
            return await _accountRepository.VerifyAccount(user);
        }

        public async Task<bool> IsAdmin(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");
            return await _accountRepository.IsAdmin(username.Trim());
        }

        public async Task<string> GetUserHighestRoleName(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");
            return await _accountRepository.GetUserHighestRoleName(username.Trim());
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");
            return await _accountRepository.IsUsernameExist(username.Trim());
        }

    }
}
