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
            return (user.Password == users.Password);
        }

        public async Task<User> GetUsers(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> IsAdmin(string username)
        {
            var isAdmin = await (from u in _dbContext.Users
                                 join ur in _dbContext.UsersRoles
                                 on u.Id equals ur.UserId
                                 where u.Username == username && ur.RoleId == 1 // 1 = Admin
                                 select ur).AnyAsync(); 

            return isAdmin;
        }

        public async Task<string> GetUserHighestRoleName(string username)
        {
            var highestRole = await (from u in _dbContext.Users
                                     join ur in _dbContext.UsersRoles
                                     on u.Id equals ur.UserId
                                     join r in _dbContext.Roles
                                     on ur.RoleId equals r.Id
                                     where u.Username == username
                                     orderby ur.RoleId // Order roles (ascending)
                                     select r.Name) // Select the role name
                                     .FirstOrDefaultAsync(); // Get the first (highest role)

            return highestRole ?? throw new Exception("No user role found for the username: " + username);
        }


        public async Task<bool> IsUsernameExist(string username)
        {
            return await (from u in _dbContext.Users
                          where u.Username == username
                          select u).AnyAsync(); 
        }


        // Dispose this context when this repository is no longer needed. To avoid increased memory usage.
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
