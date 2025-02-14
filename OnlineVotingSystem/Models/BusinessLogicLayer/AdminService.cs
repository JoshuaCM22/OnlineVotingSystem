using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.BusinessLogicLayer
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<DashboardViewModel> GetDashboardCounts()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.TotalElections = await _adminRepository.GetTotalElections();
            dashboardViewModel.TotalCandidates = await _adminRepository.GetTotalCandidates();
            dashboardViewModel.TotalVoters = await _adminRepository.GetTotalVoters();
            return dashboardViewModel;
        }

        public async Task<List<ElectionViewModel>> GetElectionList()
        {
            var response = new List<ElectionViewModel>();
            foreach (var item in await _adminRepository.GetElectionList())
            {
                var electionViewModel = new ElectionViewModel();
                electionViewModel.ID = item.Id;
                electionViewModel.Name = item.Name;
                electionViewModel.StartDateTime = item.StartDateTime;
                electionViewModel.EndDateTime = item.EndDateTime;
                electionViewModel.HasVoter = await _adminRepository.HasVoter(electionViewModel.ID);
                response.Add(electionViewModel);
            }
            return response;
        }

        public async Task<List<CandidateViewModel>> GetCandidateList()
        {
            var response = new List<CandidateViewModel>();
            var candidates = await _adminRepository.GetCandidateList();

            foreach (var item in candidates)
            {
                var candidateViewModel = new CandidateViewModel();
                candidateViewModel.ID = item.Id;
                candidateViewModel.Name = item.Name;
                candidateViewModel.Description = item.Description;
                candidateViewModel.ElectionID = item.ElectionId;
                candidateViewModel.HasVoter = await _adminRepository.HasVoter(candidateViewModel.ElectionID);
                candidateViewModel.ElectionName = await _adminRepository.GetElectionName(candidateViewModel.ElectionID);
                response.Add(candidateViewModel);
            }

            return response;
        }

        public async Task CreateNewElection(ElectionViewModel electionViewModel)
        {
            if (String.IsNullOrEmpty(electionViewModel.Name.Trim())) throw new Exception("Election Name is empty or null.");
            else if (DateTime.MinValue == electionViewModel.StartDateTime) throw new Exception("The Start Date Time is not assigned.");
            else if (DateTime.MinValue == electionViewModel.EndDateTime) throw new Exception("The End Date Time is not assigned.");

            var election = new Election();
            election.Name = electionViewModel.Name.Trim();
            election.StartDateTime = electionViewModel.StartDateTime;
            election.EndDateTime = electionViewModel.EndDateTime;
            await _adminRepository.CreateNewElection(election);
        }

        public async Task<ElectionViewModel> GetElectionByID(int electionID)
        {
            var election = await _adminRepository.GetElectionByID(electionID);
            var electionViewModel = new ElectionViewModel();
            electionViewModel.ID = electionID;
            electionViewModel.Name = election.Name.Trim();
            electionViewModel.StartDateTime = election.StartDateTime;
            electionViewModel.EndDateTime = election.EndDateTime;
            return electionViewModel;
        }

        public async Task UpdateElection(ElectionViewModel electionViewModel)
        {
            var election = new Election();
            election.Id = electionViewModel.ID;
            election.Name = electionViewModel.Name.Trim();
            election.StartDateTime = electionViewModel.StartDateTime;
            election.EndDateTime = electionViewModel.EndDateTime;
            await _adminRepository.UpdateElection(election);
        }

        public async Task DeleteElection(ElectionViewModel electionViewModel)
        {
            var election = new Election();
            election.Id = electionViewModel.ID;
            election.Name = electionViewModel.Name.Trim();
            election.StartDateTime = electionViewModel.StartDateTime;
            election.EndDateTime = electionViewModel.EndDateTime;
            await _adminRepository.DeleteElection(election);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllElections()
        {
            var elections = await _adminRepository.GetElectionList();

            var electionSelectList = elections.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return electionSelectList;
        }

        public async Task CreateNewCandidate(CandidateViewModel candidateViewModel)
        {
            if (String.IsNullOrEmpty(candidateViewModel.Name.Trim())) throw new Exception("Candidate Name is empty or null.");
            else if (String.IsNullOrEmpty(candidateViewModel.Description.Trim())) throw new Exception("Description is empty or null.");
            else if (candidateViewModel.ElectionID <= 0) throw new Exception("Election ID must be greater than 0.");

            var candidate = new Candidate();
            candidate.Name = candidateViewModel.Name.Trim();
            candidate.Description = candidateViewModel.Description.Trim();
            candidate.ElectionId = candidateViewModel.ElectionID;
            await _adminRepository.CreateNewCandidate(candidate);
        }

        public async Task<CandidateViewModel> GetCandidateByID(int candidateID)
        {
            var candidate = await _adminRepository.GetCandidateByID(candidateID);
            var candidateViewModel = new CandidateViewModel();
            candidateViewModel.ID = candidateID;
            candidateViewModel.Name = candidate.Name;
            candidateViewModel.Description = candidate.Description;
            candidateViewModel.ElectionID = candidate.ElectionId;
            return candidateViewModel;
        }

        public async Task UpdateCandidate(CandidateViewModel candidateViewModel)
        {
            var candidate = new Candidate();
            candidate.Id = candidateViewModel.ID;
            candidate.Name = candidateViewModel.Name.Trim();
            candidate.Description = candidateViewModel.Description.Trim();
            candidate.ElectionId = candidateViewModel.ElectionID;
            await _adminRepository.UpdateCandidate(candidate);
        }

        public async Task DeleteCandidate(CandidateViewModel candidateViewModel)
        {
            var candidate = new Candidate();
            candidate.Id = candidateViewModel.ID;
            candidate.Name = candidateViewModel.Name.Trim();
            candidate.Description = candidateViewModel.Description.Trim();
            candidate.ElectionId = candidateViewModel.ElectionID;
            await _adminRepository.DeleteCandidate(candidate);
        }

    }
}
