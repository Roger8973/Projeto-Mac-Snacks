using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;

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
            //var snack = _snacksRepository.Snacks;
            //return View(snack);

            var snackListViewModel = new SnackListViewModel();
            snackListViewModel.Snacks = _snacksRepository.Snacks;
            snackListViewModel.CurrentCategory = "Categoria atual";

            return View(snackListViewModel);
        }

    }
}
