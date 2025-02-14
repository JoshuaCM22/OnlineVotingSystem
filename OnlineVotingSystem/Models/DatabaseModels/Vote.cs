using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineVotingSystem.Models.DatabaseModels
{
    public partial class Vote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Election Election { get; set; }
        public virtual User User { get; set; }
    }
}
