using OnlineVotingSystem.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IResultRepository
    {
        Task<List<Election>> GetElectionList(DateTime fromDate, DateTime toDate);
        Task<List<Candidate>> GetCandidateList(int electionID);
        Task<string> GetElectionName(int electionID);
        Task<int> GetTotalVotes(int electionID, int candidateID);
        Task<string> GetElectionStatus(int electionID);
    }
}
