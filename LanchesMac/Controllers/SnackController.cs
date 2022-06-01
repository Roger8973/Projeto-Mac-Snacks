using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using LanchesMac.Models;

namespace LanchesMac.Controllers
{
    public class SnackController : Controller
    {
        private readonly ISnacksRepository _snacksRepository;

        public SnackController(ISnacksRepository snacksRepository)
        {
            _snacksRepository = snacksRepository;
        }

        public IActionResult List(string category)
        {
            IEnumerable<Snack> snacks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                snacks = _snacksRepository.Snacks.OrderBy(l => l.SnackId);
                currentCategory = "Todos os lanches";
            }

            else
            {
                snacks = _snacksRepository.Snacks
                    .Where(l => l.Category.CategoryName.Equals(category))
                    .OrderBy(c => c.Name);
            }

            currentCategory = category;
            var snackListViewModel = new SnackListViewModel()
            {
                Snacks = snacks,
                CurrentCategory = currentCategory
            };

            return View(snackListViewModel);

        }

        public IActionResult Details(int snackId)
        {
            var snack = _snacksRepository.Snacks.FirstOrDefault(l => l.SnackId == snackId);

            return View(snack);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Snack> snacks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                snacks = _snacksRepository.Snacks.OrderBy(p => p.SnackId);
                currentCategory = "Todos os lanches";
            }
            else
            {
                snacks = _snacksRepository.Snacks
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()));

                if (snacks.Any())
                    currentCategory = "Lanches";
                else
                    currentCategory = "Nenhum lanche foi encontrado";
            }

            return View("~/Views/Snack/List.cshtml", new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = currentCategory
            });
        }

    }
}
