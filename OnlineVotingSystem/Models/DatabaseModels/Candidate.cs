using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineVotingSystem.Models.DatabaseModels
{
    public partial class Candidate
    {
        public Candidate()
        {
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ElectionId { get; set; }

        public virtual Election Election { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
