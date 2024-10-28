using ChangeRequestSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

public class YourDbContext : IdentityDbContext<User>
{
    public YourDbContext() : base("YourDbContext") { }

    public DbSet<ChangeRequest> ChangeRequests { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Timeline> Timelines { get; set; }

    public static YourDbContext Create()
    {
        return new YourDbContext();
    }
}
