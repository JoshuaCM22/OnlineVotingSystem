using OnlineVotingSystem.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IAdminRepository
    {
        Task<int> GetTotalElections(DateTime fromDate, DateTime toDate);
        Task<int> GetTotalCandidates(DateTime fromDate, DateTime toDate);
        Task<int> GetTotalVoters(DateTime fromDate, DateTime toDate);
        Task<int> GetTotalUpcomingElections(DateTime fromDate, DateTime toDate);
        Task<int> GetTotalOngoingElections(DateTime fromDate, DateTime toDate);
        Task<int> GetTotalCompletedElections(DateTime fromDate, DateTime toDate);
        Task<string> GetElectionStatus(int electionID);
        Task<List<Election>> GetElectionList(DateTime fromDate, DateTime toDate);
        Task<List<Election>> GetUpComingElectionList();
        Task<List<Candidate>> GetCandidateList(DateTime fromDate, DateTime toDate, int statusId);
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
