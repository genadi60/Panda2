using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda2.Data.Models;
using Panda2.Services.Data.Contracts;

namespace Panda2.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly SignInManager<PandaUser> _signInManager;

        public UsersController(IUsersService usersService, SignInManager<PandaUser> signInManager)
        {
            this._usersService = usersService;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return Redirect("/Identity/Account/Register");
        }

        [HttpGet]
        [Authenticated]
        
        
        public IActionResult Logout()
        {
            const string cookieKey = ".AspNetCore.Identity.Application";
            var options = _usersService.ConfirmLogout();

            Response.Cookies.Delete(cookieKey, options);

            return Redirect("/Identity/Account/Logout");
        }

        public IActionResult LoggedIn()
        {
            var id = _signInManager.UserManager.GetUserId(User);

            var role = _usersService.GetRoleByUserId(id);
            
            if (role != null && role.Name.Equals("Administrator"))
            {
                var adminModel = _usersService.GetAdministratorViewModel();
                adminModel.Name = User.Identity.Name;
                return View("Admin", adminModel);
            }

            var userModel = _usersService.GetUserViewModelByIdWithPackages(id);
            return View("Logged-In", userModel);
        }
    }
}