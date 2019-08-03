using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda2.Models.InputModels;
using Panda2.Models.ViewModels;
using Panda2.Services.Contracts;

namespace Panda2.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IPackagesService _packagesService;
        private readonly IUsersService _usersService;
        
        public PackagesController(IPackagesService packages, IUsersService usersService)
        {
            _packagesService = packages;
            _usersService = usersService;
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var model = _packagesService.GetPackage(id);
            if (model != null && (User.IsInRole("Administrator") || model.Recipient.Equals(User.Identity.Name)))
            {
                return View(model);
            }
            var errorModel = new ErrorViewModel();
            errorModel.ErrorMessage = "Access denied.";
            return RedirectToAction("Error", "Home", errorModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Pending()
        {
            var model = _usersService.GetAdministratorViewModel();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Shipped()
        {
            var model = _usersService.GetAdministratorViewModel();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delivered()
        {
            var model = _usersService.GetAdministratorViewModel();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromForm]PackageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isCreated = _packagesService.Create(model);

            if (isCreated == 1)
            {
                return Redirect("/Home/Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Ship(string id)
        {
            var shipped = _packagesService.Ship(id);

            if (shipped == 1)
            {
                return RedirectToAction("Shipped");
            }
            
            return View("Pending");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Deliver(string id)
        {
            var delivered = _packagesService.Deliver(id);

            if (delivered == 1)
            {
                return RedirectToAction("Delivered");
            }

            return View("Shipped");
        }

        [Authenticated]
        public IActionResult Acquire(string id)
        {
            var acquired = _packagesService.Acquire(id);

            if (acquired == 1)
            {
                return RedirectToAction("Create", "Receipts", id);
            }

            return Redirect("/Home/Index");
        }
    }
}
