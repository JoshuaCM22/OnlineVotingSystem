﻿using OnlineVotingSystem.Models.ViewModels;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterViewModel viewModel);
        Task<bool> Login(LoginViewModel viewModel);
        Task<bool> IsAdmin(string username);
        Task<string> GetUserHighestRoleName(string username);
        Task<bool> IsUsernameExist(string username);
    }
}
