using OnlineVotingSystem.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IVoterRepository
    {
        Task<List<Election>> GetElectionList();
        Task<List<Candidate>> GetCandidateList(int electionID);
        Task<string> GetElectionName(int electionID);
        Task<string> GetCandidateName(int candidateID);
        Task<List<Vote>> GetVotesList(int userID);
        Task<List<Vote>> GetVotesList(int userID, int electionID);
        Task<int> GetUserID(string username);
        Task VoteCandidate(int electionID, int candidateID, string username);
        Task<bool> IsVoted(string username, int electionID);
    }
}
