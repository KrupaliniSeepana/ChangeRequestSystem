using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChangeRequestSystem.Models
{
    public class Timeline
    {
        public int TimelineID { get; set; }
        public int RequestID { get; set; }
        public string Stage { get; set; }
        public int AssignedUserID { get; set; } // This is the foreign key for User
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Comments { get; set; }

        // Navigation Property for ChangeRequest
        public virtual ChangeRequest ChangeRequest { get; set; }

        // Navigation Property for User
        [ForeignKey("AssignedUserID")]
        public virtual User AssignedUser { get; set; }
         // This should be named "User" to match the ForeignKey attribute
    }
}
