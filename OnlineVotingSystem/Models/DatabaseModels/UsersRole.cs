using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineVotingSystem.Models.DatabaseModels
{
    public partial class UsersRole
    {
        public int UserId { get; set; }
        public byte RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
