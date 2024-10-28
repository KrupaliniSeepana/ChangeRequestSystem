using ChangeRequestSystem.Models;
using System.Collections.Generic;
namespace ChangeRequestSystem.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
