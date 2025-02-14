using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineVotingSystem.Models.DatabaseModels
{
    public partial class Role
    {
        public Role()
        {
            UsersRoles = new HashSet<UsersRole>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
