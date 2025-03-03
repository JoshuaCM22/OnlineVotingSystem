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

        public async Task<DashboardViewModel> GetDashboardCounts(DateTime fromDate, DateTime toDate)
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.AllElections = await _adminRepository.GetTotalElections(fromDate, toDate);
            dashboardViewModel.AllCandidates = await _adminRepository.GetTotalCandidates(fromDate, toDate);
            dashboardViewModel.AllVoters = await _adminRepository.GetTotalVoters(fromDate, toDate);
            dashboardViewModel.UpcomingElections = await _adminRepository.GetTotalUpcomingElections(fromDate, toDate);
            dashboardViewModel.OngoingElections = await _adminRepository.GetTotalOngoingElections(fromDate, toDate);
            dashboardViewModel.CompletedElections = await _adminRepository.GetTotalCompletedElections(fromDate, toDate);
            return dashboardViewModel;
        }

        public async Task<List<ElectionViewModel>> GetElectionList(DateTime fromDate, DateTime toDate, int statusId)
        {
            var response = new List<ElectionViewModel>();
            var elections = await _adminRepository.GetElectionList(fromDate, toDate);

            if (statusId < 0) throw new ArgumentException("The statusId cannot be less than zero.");

            if (statusId > 0)
            {
                var dateTimeToday = DateTime.Now;
                if (statusId == 1) elections = elections.Where(x => x.StartDateTime > dateTimeToday).ToList(); // 1 = Upcoming
                else if (statusId == 2) elections = elections.Where(x => x.StartDateTime <= dateTimeToday && x.EndDateTime >= dateTimeToday).ToList(); // 2 = Ongoing
                else if (statusId == 3) elections = elections.Where(x => x.EndDateTime < dateTimeToday).ToList(); // 3 = Completed
            }

            foreach (var item in elections)
            {
                var electionViewModel = new ElectionViewModel();
                electionViewModel.ID = item.Id;
                electionViewModel.Name = item.Name.Trim();
                electionViewModel.StartDateTime = item.StartDateTime;
                electionViewModel.EndDateTime = item.EndDateTime;
                electionViewModel.Status = await _adminRepository.GetElectionStatus(electionViewModel.ID);
                response.Add(electionViewModel);
            }
            return response;
        }

        public async Task<List<CandidateViewModel>> GetCandidateList(DateTime fromDate, DateTime toDate, int statusId)
        {
            if (statusId < 0) throw new ArgumentException("The statusId cannot be less than zero.");

            var response = new List<CandidateViewModel>();
            var candidates = await _adminRepository.GetCandidateList(fromDate, toDate, statusId);

       
            foreach (var item in candidates)
            {
                var candidateViewModel = new CandidateViewModel();
                candidateViewModel.ID = item.Id;
                candidateViewModel.Name = item.Name.Trim();
                candidateViewModel.Description = item.Description.Trim();
                candidateViewModel.ElectionID = item.ElectionId;
                candidateViewModel.ElectionStatus = (await _adminRepository.GetElectionStatus(item.ElectionId)).Trim();
                candidateViewModel.ElectionName = (await _adminRepository.GetElectionName(candidateViewModel.ElectionID)).Trim();
                response.Add(candidateViewModel);
            }

            return response;
        }

        public async Task CreateNewElection(ElectionViewModel electionViewModel)
        {
            if (String.IsNullOrEmpty(electionViewModel.Name.Trim())) throw new ArgumentException("The Name cannot be null or empty string.");
            else if (DateTime.MinValue == electionViewModel.StartDateTime) throw new ArgumentException("The StartDateTime cannot use the default date time value.");
            else if (DateTime.MinValue == electionViewModel.EndDateTime) throw new ArgumentException("The EndDateTime cannot use the default date time value.");

            var election = new Election();
            election.Name = electionViewModel.Name.Trim();
            election.StartDateTime = electionViewModel.StartDateTime;
            election.EndDateTime = electionViewModel.EndDateTime;
            await _adminRepository.CreateNewElection(election);
        }

        public async Task<ElectionViewModel> GetElectionByID(int electionID)
        {
            if (electionID <= 0) throw new ArgumentException("The electionID cannot be less than or equal to zero.");

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
            if (electionViewModel.ID <= 0) throw new ArgumentException("The ID cannot be less than or equal to zero.");
            else if (String.IsNullOrEmpty(electionViewModel.Name.Trim())) throw new ArgumentException("The Name cannot be null or empty string.");
            else if (DateTime.MinValue == electionViewModel.StartDateTime) throw new ArgumentException("The StartDateTime cannot use the default date time value.");
            else if (DateTime.MinValue == electionViewModel.EndDateTime) throw new ArgumentException("The EndDateTime cannot use the default date time value.");

            var election = new Election();
            election.Id = electionViewModel.ID;
            election.Name = electionViewModel.Name.Trim();
            election.StartDateTime = electionViewModel.StartDateTime;
            election.EndDateTime = electionViewModel.EndDateTime;
            await _adminRepository.UpdateElection(election);
        }

        public async Task DeleteElection(ElectionViewModel electionViewModel)
        {
            if (electionViewModel.ID <= 0) throw new ArgumentException("The ID cannot be less than or equal to zero.");
            else if (String.IsNullOrEmpty(electionViewModel.Name.Trim())) throw new ArgumentException("The Name cannot be null or empty string.");
            else if (DateTime.MinValue == electionViewModel.StartDateTime) throw new ArgumentException("The StartDateTime cannot use the default date time value.");
            else if (DateTime.MinValue == electionViewModel.EndDateTime) throw new ArgumentException("The EndDateTime cannot use the default date time value.");

            var election = new Election();
            election.Id = electionViewModel.ID;
            election.Name = electionViewModel.Name.Trim();
            election.StartDateTime = electionViewModel.StartDateTime;
            election.EndDateTime = electionViewModel.EndDateTime;
            await _adminRepository.DeleteElection(election);
        }

        public async Task<IEnumerable<SelectListItem>> GetUpComingElections()
        {
            var elections = await _adminRepository.GetUpComingElectionList();

            var electionSelectList = elections.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name.Trim()
            });

            return electionSelectList;
        }

        public IEnumerable<SelectListItem> GetAllElectionStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "All" },
                new SelectListItem { Value = "1", Text = "Upcoming" },
                new SelectListItem { Value = "2", Text = "Ongoing" },
                new SelectListItem { Value = "3", Text = "Completed" }
            };
        }


        public async Task CreateNewCandidate(CandidateViewModel candidateViewModel)
        {
            if (String.IsNullOrEmpty(candidateViewModel.Name)) throw new ArgumentException("The Name cannot be null or empty string.");
            else if (String.IsNullOrEmpty(candidateViewModel.Description)) throw new ArgumentException("The Description cannot be null or empty string.");
            else if (candidateViewModel.ElectionID <= 0) throw new ArgumentException("The ElectionID cannot be less than or equal to zero.");

            var candidate = new Candidate();
            candidate.Name = candidateViewModel.Name.Trim();
            candidate.Description = candidateViewModel.Description.Trim();
            candidate.ElectionId = candidateViewModel.ElectionID;
            await _adminRepository.CreateNewCandidate(candidate);
        }

        public async Task<CandidateViewModel> GetCandidateByID(int candidateID)
        {
            if (candidateID <= 0) throw new ArgumentException("The candidateID cannot be less than or equal to zero.");

            var candidate = await _adminRepository.GetCandidateByID(candidateID);
            var candidateViewModel = new CandidateViewModel();
            candidateViewModel.ID = candidateID;
            candidateViewModel.Name = candidate.Name.Trim();
            candidateViewModel.Description = candidate.Description.Trim();
            candidateViewModel.ElectionID = candidate.ElectionId;
            return candidateViewModel;
        }

        public async Task UpdateCandidate(CandidateViewModel candidateViewModel)
        {
            if (candidateViewModel.ID <= 0) throw new ArgumentException("The ID cannot be less than or equal to zero.");
            else if (String.IsNullOrEmpty(candidateViewModel.Name)) throw new ArgumentException("The Name cannot be null or empty string.");
            else if (String.IsNullOrEmpty(candidateViewModel.Description)) throw new ArgumentException("The Description cannot be null or empty string.");
            else if (candidateViewModel.ElectionID <= 0) throw new ArgumentException("The ElectionID cannot be less than or equal to zero.");

            var candidate = new Candidate();
            candidate.Id = candidateViewModel.ID;
            candidate.Name = candidateViewModel.Name.Trim();
            candidate.Description = candidateViewModel.Description.Trim();
            candidate.ElectionId = candidateViewModel.ElectionID;
            await _adminRepository.UpdateCandidate(candidate);
        }

        public async Task DeleteCandidate(CandidateViewModel candidateViewModel)
        {
            if (candidateViewModel.ID <= 0) throw new ArgumentException("The ID cannot be less than or equal to zero.");
            else if (String.IsNullOrEmpty(candidateViewModel.Name)) throw new ArgumentException("The Name cannot be null or empty string.");
            else if (String.IsNullOrEmpty(candidateViewModel.Description)) throw new ArgumentException("The Description cannot be null or empty string.");
            else if (candidateViewModel.ElectionID <= 0) throw new ArgumentException("The ElectionID cannot be less than or equal to zero.");

            var candidate = new Candidate();
            candidate.Id = candidateViewModel.ID;
            candidate.Name = candidateViewModel.Name.Trim();
            candidate.Description = candidateViewModel.Description.Trim();
            candidate.ElectionId = candidateViewModel.ElectionID;
            await _adminRepository.DeleteCandidate(candidate);
        }

    }
}
