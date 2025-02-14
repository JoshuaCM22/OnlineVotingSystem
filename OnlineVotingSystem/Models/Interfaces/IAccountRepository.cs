using OnlineVotingSystem.Models.DatabaseModels;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAccount(User users);
        Task<bool> VerifyAccount(User users);
        Task<User> GetUsers(string username);
        Task<bool> IsAdmin(string username);
        Task<string> GetUserHighestRoleName(string username);
        Task<bool> IsUsernameExist(string username);
    }
}
