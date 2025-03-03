using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineVotingSystem.Models.DatabaseModels
{
    public partial class User
    {
        public User()
        {
            UsersRoles = new HashSet<UsersRole>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public virtual ICollection<UsersRole> UsersRoles { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
