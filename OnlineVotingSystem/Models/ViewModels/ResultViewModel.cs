namespace OnlineVotingSystem.Models.ViewModels
{
    public class ResultViewModel
    {
        public int CandidateID { get; set; }

        public string CandidateName { get; set; }

        public string Description { get; set; }

        public int TotalVotes { get; set; }
    }
}
