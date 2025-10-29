using Microsoft.AspNetCore.Mvc;

namespace BurnoutSurveyMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
