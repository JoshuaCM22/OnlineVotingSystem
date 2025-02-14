using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IAdminService
    {
        Task<DashboardViewModel> GetDashboardCounts();
        Task<List<ElectionViewModel>> GetElectionList();
        Task<List<CandidateViewModel>> GetCandidateList();
        Task CreateNewElection(ElectionViewModel electionViewModel);
        Task<ElectionViewModel> GetElectionByID(int electionID);
        Task UpdateElection(ElectionViewModel electionViewModel);
        Task DeleteElection(ElectionViewModel electionViewModel);
        Task<IEnumerable<SelectListItem>> GetAllElections();
        Task CreateNewCandidate(CandidateViewModel candidateViewModel);
        Task<CandidateViewModel> GetCandidateByID(int candidateID);
        Task UpdateCandidate(CandidateViewModel candidateViewModel);
        Task DeleteCandidate(CandidateViewModel candidateViewModel);
    }
}
