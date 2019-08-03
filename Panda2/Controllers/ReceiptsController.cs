using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda2.Models;
using Panda2.Models.ViewModels;
using Panda2.Services.Contracts;

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
            var model = _receiptsService.GetUserViewModelByIdWithReceipts(id);
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
            var errorModel = new ErrorViewModel();
            errorModel.ErrorMessage = "Access denied.";
            return RedirectToAction("Error", "Home", errorModel);
        }

        [Authenticated]
        public IActionResult Create(string id)
        {
            var userId = _userManager.GetUserId(User);
            bool isCreated = _receiptsService.Create(id, userId);

            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            var errorModel = new ErrorViewModel();
            errorModel.ErrorMessage = "Access denied.";
            return RedirectToAction("Error", "Home", errorModel);
        }
    }
}
