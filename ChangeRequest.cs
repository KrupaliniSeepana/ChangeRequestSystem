using ChangeRequestSystem.Models;
using System;
namespace ChangeRequestSystem.Models
{
    public class ChangeRequest
    {
        public int RequestID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProjectID { get; set; }
        public int RequestedBy { get; set; }
        public int ApprovedBy { get; set; }
        public int AssignedTeamID { get; set; }

        // Add this Comments property
        public string Comments { get; set; }

        public virtual User Requester { get; set; }
        public virtual User Manager { get; set; }
        public virtual Team AssignedTeam { get; set; }
    }
}
