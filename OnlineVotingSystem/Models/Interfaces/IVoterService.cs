using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Voter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IVoterService
    {
        Task<List<ElectionViewModel>> GetElectionList(string username);
        Task<List<CandidateViewModel>> GetCandidateList(int electionID);
        Task<string> GetElectionName(int electionID);
        Task VoteCandidate(int electionID, int candidateID, string username);
        Task<List<VotedHistoryViewModel>> GetVotedHistory(string username, int electionID, DateTime fromDate, DateTime toDate);
    }
}
