using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.DataAccessLayer
{
    public class ResultRepository : IResultRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public ResultRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Election>> GetElectionList()
        {
            DateTime currentDateTime = DateTime.Now;
            return await _dbContext.Elections.Where(e => e.StartDateTime <= currentDateTime).ToListAsync();
        }

        public async Task<List<Candidate>> GetCandidateList(int electionID)
        {
            return await _dbContext.Candidates.Where(x => x.ElectionId == electionID).ToListAsync();
        }

        public async Task<string> GetElectionName(int electionID)
        {
            return (await _dbContext.Elections.Where(x => x.Id == electionID).SingleOrDefaultAsync()).Name;
        }

        public async Task<int> GetTotalVotes(int electionID, int candidateID)
        {
            return await _dbContext.Votes.Where(x => x.ElectionId == electionID && x.CandidateId == candidateID).CountAsync();
        }

        // Dispose this context when this repository is no longer needed. To avoid increased memory usage.
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
