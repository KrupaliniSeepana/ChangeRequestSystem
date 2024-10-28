using ChangeRequestSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

public class User : IdentityUser
{
    public int ProjectID { get; set; }
    public virtual Project Project { get; set; }
}
