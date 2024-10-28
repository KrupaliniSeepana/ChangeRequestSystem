using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

public class DbInitializer : DropCreateDatabaseIfModelChanges<YourDbContext>
{
    protected override void Seed(YourDbContext context)
    {
        // Initialize Role Manager and User Manager
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        var userManager = new UserManager<User>(new UserStore<User>(context));

        // Seed Roles
        string[] roleNames = { "Requester", "Manager", "Developer" };
        foreach (var roleName in roleNames)
        {
            if (!roleManager.RoleExists(roleName))
            {
                var role = new IdentityRole(roleName);
                roleManager.Create(role);
            }
        }

        // Seed Users
        // Create a user for each role
        CreateUserIfNotExists(userManager, "requester@example.com", "Password123!", "Requester");
        CreateUserIfNotExists(userManager, "manager@example.com", "Password123!", "Manager");
        CreateUserIfNotExists(userManager, "developer@example.com", "Password123!", "Developer");

        base.Seed(context);
    }

    private void CreateUserIfNotExists(UserManager<User> userManager, string email, string password, string roleName)
    {
        if (userManager.FindByEmail(email) == null)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
            };

            var result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}
