using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.DataAccessLayer
{
    public class AdminRepository : IAdminRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalElections(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Elections
                .Where(x => x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                .CountAsync();
        }


        public async Task<int> GetTotalCandidates(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Candidates
                .Where(x => x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                .CountAsync();
        }

        public async Task<int> GetTotalVoters(DateTime fromDate, DateTime toDate)
        {
            var response = await (from u in _dbContext.Users
                                  join ur in _dbContext.UsersRoles
                                    on u.Id equals ur.UserId
                                  where ur.RoleId == 2 // 2 = Voter
                                        && u.DateTimeCreated >= fromDate
                                        && u.DateTimeCreated < toDate
                                  select new
                                  {
                                      UserID = u.Id,
                                      Username = u.Username,
                                      RoleID = ur.RoleId,
                                      Created = u.DateTimeCreated
                                  }).CountAsync();
            return response;
        }

        public async Task<int> GetTotalUpcomingElections(DateTime fromDate, DateTime toDate)
        {
            var dateTimeToday = DateTime.Now;
            return await _dbContext.Elections
                   .Where(x => x.StartDateTime > dateTimeToday && x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                   .CountAsync();
        }

        public async Task<int> GetTotalOngoingElections(DateTime fromDate, DateTime toDate)
        {
            var dateTimeToday = DateTime.Now;
            return await _dbContext.Elections
                   .Where(x => x.StartDateTime <= dateTimeToday && x.EndDateTime >= dateTimeToday && x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                   .CountAsync();
        }

        public async Task<int> GetTotalCompletedElections(DateTime fromDate, DateTime toDate)
        {
            var dateTimeToday = DateTime.Now;
            return await _dbContext.Elections
                  .Where(x => x.EndDateTime < dateTimeToday && x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                  .CountAsync();
        }


        public async Task<List<Election>> GetElectionList(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Elections.Where(x => x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date).ToListAsync();
        }

        public async Task<List<Election>> GetUpComingElectionList()
        {
            var dateTimeToday = DateTime.Now;
            return await _dbContext.Elections.Where(x => x.StartDateTime > dateTimeToday).ToListAsync();
        }

        public async Task<List<Candidate>> GetCandidateList(DateTime fromDate, DateTime toDate, int statusId)
        {
            var response = await (from c in _dbContext.Candidates
                                  join e in _dbContext.Elections
                                  on c.ElectionId equals e.Id
                                  where c.DateTimeCreated >= fromDate
                                     && c.DateTimeCreated <= toDate
                                  select new
                                  {
                                      C = c,
                                      E = e
                                  }).ToListAsync();


            if (statusId > 0)
            {
                var dateTimeToday = DateTime.Now;
                if (statusId == 1) response = response.Where(x => x.E.StartDateTime > dateTimeToday).ToList(); // 1 = Upcoming
                else if (statusId == 2) response = response.Where(x => x.E.StartDateTime <= dateTimeToday && x.E.EndDateTime >= dateTimeToday).ToList(); // 2 = Ongoing
                else if (statusId == 3) response = response.Where(x => x.E.EndDateTime < dateTimeToday).ToList(); // 3 = Completed
            }

            return response.Select(x => x.C).ToList();
        }

        public async Task<string> GetElectionStatus(int electionID)
        {
            var dateTimeToday = DateTime.Now;
            var election = await _dbContext.Elections.Where(x => x.Id == electionID).SingleOrDefaultAsync();
            DateTime startDate = election.StartDateTime;
            DateTime endDate = election.EndDateTime;
            if (startDate > dateTimeToday) return "Upcoming";
            else if (startDate <= dateTimeToday && endDate >= dateTimeToday) return "Ongoing";
            else if (endDate < dateTimeToday) return "Completed";
            else throw new Exception("Incorrect Election Status");
        }

        public async Task<string> GetElectionName(int electionID)
        {
            return (await _dbContext.Elections.Where(x => x.Id == electionID).SingleOrDefaultAsync()).Name;
        }

        public async Task CreateNewElection(Election election)
        {
            await _dbContext.Elections.AddAsync(election);
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }

        public async Task<Election> GetElectionByID(int electionID)
        {
            return await _dbContext.Elections.Where(x => x.Id == electionID).SingleOrDefaultAsync();
        }

        public async Task UpdateElection(Election election)
        {
            _dbContext.Entry(election).State = EntityState.Modified;
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }

        public async Task DeleteElection(Election election)
        {
            _dbContext.Entry(election).State = EntityState.Deleted;
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }

        public async Task CreateNewCandidate(Candidate candidate)
        {
            await _dbContext.Candidates.AddAsync(candidate);
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }

        public async Task<Candidate> GetCandidateByID(int candidateID)
        {
            return await _dbContext.Candidates.Where(x => x.Id == candidateID).SingleOrDefaultAsync();
        }

        public async Task UpdateCandidate(Candidate candidate)
        {
            _dbContext.Entry(candidate).State = EntityState.Modified;
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }

        public async Task DeleteCandidate(Candidate candidate)
        {
            _dbContext.Entry(candidate).State = EntityState.Deleted;
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }


        // Dispose this context when this repository is no longer needed. To avoid increased memory usage.
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
