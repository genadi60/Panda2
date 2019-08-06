using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda2.Data.Models;
using Panda2.Data.Models.ViewModels;
using Panda2.Services.Data.Contracts;

namespace Panda2.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService _receiptsService;
        private readonly UserManager<PandaUser> _userManager;

        public ReceiptsController(IReceiptsService receiptsService, UserManager<PandaUser> userManager)
        {
            _receiptsService = receiptsService;
            _userManager = userManager;
        }

        [Authenticated]
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(User);
            var model = _receiptsService.GetUserViewModelByIdWithReceipts<UserViewModel, ReceiptViewModel>(id);
            return View(model);
        }

        [Authenticated]
        public IActionResult Details(string id)
        {
            var model = _receiptsService.GetReceiptViewModel(id);
            if (model != null &&  model.Recipient.Equals(User.Identity.Name))
            {
                return View(model);
            }
            var errorModel = new ErrorViewModel { ErrorMessage = "Access denied." };
            return RedirectToAction("Error", "Home", errorModel);
        }

        [Authenticated]
        public IActionResult Create(string id)
        {
            var userId = _userManager.GetUserId(User);
            var result = _receiptsService.Create(id, userId).Result;

            if (result == 2)
            {
                return RedirectToAction("Index");
            }

            var errorModel = new ErrorViewModel { ErrorMessage = "Access denied." };
            return RedirectToAction("Error", "Home", errorModel);
        }
    }
}
