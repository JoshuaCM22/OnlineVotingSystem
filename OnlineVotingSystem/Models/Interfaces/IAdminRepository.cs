using OnlineVotingSystem.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IAdminRepository
    {
        Task<int> GetTotalElections();
        Task<int> GetTotalCandidates();
        Task<int> GetTotalVoters();
        Task<List<Election>> GetElectionList();
        Task<List<Candidate>> GetCandidateList();
        Task<bool> HasVoter(int electionID);
        Task<string> GetElectionName(int electionID);
        Task CreateNewElection(Election election);
        Task<Election> GetElectionByID(int electionID);
        Task UpdateElection(Election election);
        Task DeleteElection(Election election);
        Task CreateNewCandidate(Candidate candidate);
        Task<Candidate> GetCandidateByID(int candidateID);
        Task UpdateCandidate(Candidate candidate);
        Task DeleteCandidate(Candidate candidate);
    }
}
