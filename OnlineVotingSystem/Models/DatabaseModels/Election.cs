using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineVotingSystem.Models.DatabaseModels
{
    public partial class Election
    {
        public Election()
        {
            Candidates = new HashSet<Candidate>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
