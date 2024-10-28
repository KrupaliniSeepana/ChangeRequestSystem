// Team.cs
using System.Collections.Generic;
using ChangeRequestSystem.Models;

namespace ChangeRequestSystem.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }

        // Add any additional properties or relationships as needed
        public virtual ICollection<ChangeRequest> ChangeRequests { get; set; }
    }
}
