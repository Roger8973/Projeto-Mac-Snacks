using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Controllers
{
    public class SnackController : Controller
    {
        private readonly ISnacksRepository _snacksRepository;

        public SnackController(ISnacksRepository snacksRepository)
        {
            _snacksRepository = snacksRepository;
        }

        public IActionResult List()
        {
            var snack = _snacksRepository.Snacks;
            return View(snack);
        }
    }
}
