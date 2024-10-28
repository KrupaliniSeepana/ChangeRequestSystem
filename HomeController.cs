using System.Web.Mvc;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        ViewBag.Title = "Home Page"; // Set the ViewBag value here
        return View();
    }
}
