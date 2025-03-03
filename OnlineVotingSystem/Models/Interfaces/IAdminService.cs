using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IAdminService
    {
        Task<DashboardViewModel> GetDashboardCounts(DateTime fromDate, DateTime toDate);
        Task<List<ElectionViewModel>> GetElectionList(DateTime fromDate, DateTime toDate, int statusId);
        Task<List<CandidateViewModel>> GetCandidateList(DateTime fromDate, DateTime toDate, int statusId);
        Task CreateNewElection(ElectionViewModel electionViewModel);
        Task<ElectionViewModel> GetElectionByID(int electionID);
        Task UpdateElection(ElectionViewModel electionViewModel);
        Task DeleteElection(ElectionViewModel electionViewModel);
        Task<IEnumerable<SelectListItem>> GetUpComingElections();
        IEnumerable<SelectListItem> GetAllElectionStatuses();
        Task CreateNewCandidate(CandidateViewModel candidateViewModel);
        Task<CandidateViewModel> GetCandidateByID(int candidateID);
        Task UpdateCandidate(CandidateViewModel candidateViewModel);
        Task DeleteCandidate(CandidateViewModel candidateViewModel);
    }
}
