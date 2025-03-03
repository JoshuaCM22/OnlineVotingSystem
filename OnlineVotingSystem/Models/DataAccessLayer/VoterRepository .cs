using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.DataAccessLayer
{
    public class VoterRepository : IVoterRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public VoterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Election>> GetElectionList()
        {
            DateTime currentDateTime = DateTime.Now;
            return await _dbContext.Elections.Where(e => e.StartDateTime <= currentDateTime && e.EndDateTime >= currentDateTime).ToListAsync();
        }

        public async Task<List<Candidate>> GetCandidateList(int electionID)
        {
            return await _dbContext.Candidates.Where(x => x.ElectionId == electionID).ToListAsync();
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

        public async Task<string> GetCandidateName(int candidateID)
        {
            return (await _dbContext.Candidates.Where(x => x.Id == candidateID).SingleOrDefaultAsync()).Name;
        }

        public async Task<List<Vote>> GetVotesList(int userID, DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Votes
                     .Where(x => x.UserId == userID && x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                      .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Vote>> GetVotesList(int userID, int electionID, DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.Votes
                .Where(x => x.UserId == userID && x.ElectionId == electionID && x.DateTimeCreated.Date >= fromDate.Date && x.DateTimeCreated.Date <= toDate.Date)
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<int> GetUserID(string username)
        {
            return (await _dbContext.Users.Where(x => x.Username == username).SingleOrDefaultAsync()).Id;
        }

        public async Task VoteCandidate(int electionID, int candidateID, string username)
        {
            var vote = new Vote();
            vote.ElectionId = electionID;
            vote.CandidateId = candidateID;
            vote.UserId = await GetUserID(username);
            vote.DateTimeCreated = DateTime.Now;
            await _dbContext.Votes.AddAsync(vote);
            if (await _dbContext.SaveChangesAsync() <= 0) throw new Exception("No rows affected.");
        }

        public async Task<bool> IsVoted(string username, int electionID)
        {
            var isVoted = await (from u in _dbContext.Users
                                 join v in _dbContext.Votes
                                 on u.Id equals v.UserId
                                 where u.Username == username && v.ElectionId == electionID
                                 select v).AnyAsync(); 

            return isVoted;
        }

        // Dispose this context when this repository is no longer needed. To avoid increased memory usage.
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
