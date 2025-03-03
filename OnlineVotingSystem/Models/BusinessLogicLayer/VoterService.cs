using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.Models.ViewModels;
using OnlineVotingSystem.Models.ViewModels.Voter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.BusinessLogicLayer
{
    public class VoterService : IVoterService
    {
        private readonly IVoterRepository _voterRepository;

        public VoterService(IVoterRepository voterRepository)
        {
            _voterRepository = voterRepository;
        }


        public async Task<List<ElectionViewModel>> GetElectionList(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");

            var response = new List<ElectionViewModel>();
            foreach (var item in await _voterRepository.GetElectionList())
            {
                var electionViewModel = new ElectionViewModel();
                electionViewModel.ID = item.Id;
                electionViewModel.Name = item.Name.Trim();
                electionViewModel.StartDateTime = item.StartDateTime;
                electionViewModel.EndDateTime = item.EndDateTime;
                electionViewModel.IsVoted = await _voterRepository.IsVoted(username, electionViewModel.ID);
                response.Add(electionViewModel);
            }
            return response;
        }


        public async Task<List<CandidateViewModel>> GetCandidateList(int electionID)
        {
            if (electionID <= 0) throw new ArgumentException("The electionID cannot be less than or equal to zero.");

            var response = new List<CandidateViewModel>();
            var candidates = await _voterRepository.GetCandidateList(electionID);

            foreach (var item in candidates)
            {
                var candidateViewModel = new CandidateViewModel();
                candidateViewModel.ID = item.Id;
                candidateViewModel.Name = item.Name.Trim();
                candidateViewModel.Description = item.Description.Trim();
                candidateViewModel.ElectionID = item.ElectionId;
                candidateViewModel.ElectionName = await _voterRepository.GetElectionName(candidateViewModel.ElectionID);
                response.Add(candidateViewModel);
            }

            return response;
        }


        public async Task<List<VotedHistoryViewModel>> GetVotedHistory(string username, int electionID, DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");
            else if (electionID < 0) throw new ArgumentException("The electionID cannot be less than zero.");

            var response = new List<VotedHistoryViewModel>();
            int userID = await _voterRepository.GetUserID(username);
            List<Vote> votes = (electionID == 0) ? await _voterRepository.GetVotesList(userID, fromDate, toDate) : await _voterRepository.GetVotesList(userID, electionID, fromDate, toDate);

            foreach (var vote in votes)
            {
                var votedHistoryViewModel = new VotedHistoryViewModel();
                votedHistoryViewModel.ElectionName = await _voterRepository.GetElectionName(vote.ElectionId);
                votedHistoryViewModel.ElectionStatus = await _voterRepository.GetElectionStatus(vote.ElectionId);
                votedHistoryViewModel.CandidateName = await _voterRepository.GetCandidateName(vote.CandidateId);
                votedHistoryViewModel.DateTimeVoted = vote.DateTimeCreated;
                response.Add(votedHistoryViewModel);
            }

            return response;
        }

        public async Task<string> GetElectionName(int electionID)
        {
            if (electionID <= 0) throw new ArgumentException("The electionID cannot be less than or equal to zero.");

            return await _voterRepository.GetElectionName(electionID);
        }

        public async Task VoteCandidate(int electionID, int candidateID, string username)
        {
            if (electionID <= 0) throw new ArgumentException("The electionID cannot be less than or equal to zero.");
            else if (candidateID <= 0) throw new ArgumentException("The candidateID cannot be less than or equal to zero.");
            else if (string.IsNullOrEmpty(username)) throw new Exception("The username cannot be null or empty string.");

            await _voterRepository.VoteCandidate(electionID, candidateID, username);
        }

    }
}
