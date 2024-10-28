using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ChangeRequestSystem.Models;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;

    public AccountController()
    {
        _userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();
    }

    // GET: /Account/Login
    [AllowAnonymous]
    public ActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindAsync(model.Email, model.Password);
        if (user != null)
        {
            await SignInAsync(user, model.RememberMe);
            return RedirectToLocal(returnUrl);
        }
        else
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
    }

    private async Task SignInAsync(User user, bool isPersistent)
    {
        var authenticationManager = HttpContext.GetOwinContext().Authentication;
        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
    }

    // Define the RedirectToLocal method here
    protected ActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home"); // Change to your default action
        }
    }
}
