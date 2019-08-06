using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Panda2.Data.Models;
using Panda2.Data.Models.ViewModels;

namespace Panda2.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<PandaUser> _signInManager;

        public HomeController(SignInManager<PandaUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
               return Redirect("/Users/LoggedIn");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel model)
        {
            model.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(model);
        }
    }
}
