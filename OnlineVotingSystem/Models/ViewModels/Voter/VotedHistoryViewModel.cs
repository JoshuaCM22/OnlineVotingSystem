using System;

namespace OnlineVotingSystem.Models.ViewModels.Voter
{
    public class VotedHistoryViewModel
    {
        public string ElectionName { get; set; }
        public string ElectionStatus { get; set; }
        public string CandidateName { get; set; }
        public DateTime DateTimeVoted { get; set; }
    }
}
