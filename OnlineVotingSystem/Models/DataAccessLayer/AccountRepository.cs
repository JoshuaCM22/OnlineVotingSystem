using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.DataAccessLayer
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAccount(User user)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Users.AddAsync(user);
                    if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");

                    int userID = user.Id;

                    var usersRoles = new UsersRole();
                    usersRoles.UserId = userID;
                    usersRoles.RoleId = 2; // 2 = Voter

                    await _dbContext.UsersRoles.AddAsync(usersRoles); // 1 = Insert
                    if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }

        }


        public async Task<bool> VerifyAccount(User users)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(s => s.Username == users.Username);
            if (user == null) return false;
            if (user.Password == users.Password) return true;
            return false;
        }

        public async Task<User> GetUsers(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> IsAdmin(string username)
        {
            int userID = (await _dbContext.Users.SingleOrDefaultAsync(s => s.Username == username)).Id;
            var userRoles = await _dbContext.UsersRoles.SingleOrDefaultAsync(s => s.UserId == userID && s.RoleId == 1); // 1 = Admin
            return (userRoles != null);
        }


        public async Task<string> GetUserHighestRoleName(string username)
        {
            int userID = (await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username)).Id;
            int roleID = await _dbContext.UsersRoles.Where(x => x.UserId == userID).OrderBy(x => x.RoleId).Select(x => x.RoleId).FirstOrDefaultAsync();
            return (await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == roleID)).Name;
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            int count = await _dbContext.Users.CountAsync(x => x.Username == username);
            return (count > 0);
        }

        // Dispose this context when this repository is no longer needed. To avoid increased memory usage.
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
