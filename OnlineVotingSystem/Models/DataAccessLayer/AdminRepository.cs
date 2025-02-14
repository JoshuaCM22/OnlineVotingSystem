using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Admin;
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

        public async Task<int> GetTotalElections()
        {
            return await _dbContext.Elections.CountAsync();
        }

        public async Task<int> GetTotalCandidates()
        {
            return await _dbContext.Candidates.CountAsync();
        }

        public async Task<int> GetTotalVoters()
        {
            return await _dbContext.UsersRoles.CountAsync(x => x.RoleId == 2); // 2 = Voter
        }

        public async Task<List<Election>> GetElectionList()
        {
            return await _dbContext.Elections.ToListAsync();
        }

        public async Task<bool> HasVoter(int electionID)
        {
            return (await _dbContext.Votes.Where(x=>x.ElectionId == electionID).CountAsync() > 0);
        }

        public async Task<List<Candidate>> GetCandidateList()
        {
            return await _dbContext.Candidates.ToListAsync();
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
