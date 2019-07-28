using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda2.Models;
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

        public IActionResult Index()
        {
            var id = _userManager.GetUserId(User);
            var model = _receiptsService.GetUserViewModelByIdWithReceipts(id);
            return View(model);
        }

        public IActionResult Details(string id)
        {
            var model = _receiptsService.GetReceiptViewModel(id);
            return View(model);
        }

        public IActionResult Create(string id)
        {
            bool isCreated = _receiptsService.Create(id);

            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
