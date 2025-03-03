namespace OnlineVotingSystem.Models.ViewModels.Admin
{
    public class DashboardViewModel
    {
        public int AllElections { get; set; }
        public int AllCandidates { get; set; }
        public int AllVoters { get; set; }
        public int UpcomingElections { get; set; }
        public int OngoingElections { get; set; }
        public int CompletedElections { get; set; }
    }
}
