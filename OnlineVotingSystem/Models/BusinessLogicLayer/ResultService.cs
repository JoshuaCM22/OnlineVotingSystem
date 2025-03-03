using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.BusinessLogicLayer
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }


        public async Task<List<ElectionViewModel>> GetElectionList(DateTime fromDate, DateTime toDate)
        {
            var response = new List<ElectionViewModel>();
            foreach (var item in await _resultRepository.GetElectionList(fromDate, toDate))
            {
                var electionViewModel = new ElectionViewModel();
                electionViewModel.ID = item.Id;
                electionViewModel.Name = item.Name.Trim();
                electionViewModel.Status = await _resultRepository.GetElectionStatus(electionViewModel.ID);
                electionViewModel.StartDateTime = item.StartDateTime;
                electionViewModel.EndDateTime = item.EndDateTime;
                response.Add(electionViewModel);
            }
            return response;
        }

        public async Task<List<ResultViewModel>> GetCandidateList(int electionID)
        {
            if (electionID <= 0) throw new ArgumentException("The electionID cannot be less than or equal to zero.");

            var response = new List<ResultViewModel>();
            var candidates = await _resultRepository.GetCandidateList(electionID);
            string electionName = await _resultRepository.GetElectionName(electionID);

            foreach (var candidate in candidates)
            {
                var resultViewModel = new ResultViewModel();
                resultViewModel.CandidateID = candidate.Id;
                resultViewModel.CandidateName = candidate.Name;
                resultViewModel.Description = candidate.Description;
                resultViewModel.TotalVotes = await _resultRepository.GetTotalVotes(electionID, resultViewModel.CandidateID);
                response.Add(resultViewModel);
            }

            response = response.OrderByDescending(x => x.TotalVotes).ToList();

            return response;
        }

        public async Task<string> GetElectionName(int electionID)
        {
            if (electionID <= 0) throw new ArgumentException("The electionID cannot be less than or equal to zero.");

            return (await _resultRepository.GetElectionName(electionID)).Trim();
        }
    }
}
