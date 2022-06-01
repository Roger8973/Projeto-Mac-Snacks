using Microsoft.AspNetCore.Mvc;
using LanchesMac.Repositories.Interfaces;
namespace LanchesMac.Components
{
    public class CategoryMenu : ViewComponent 
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoryMenu(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var category = _categoriesRepository.Categories.OrderBy(c => c.CategoryName);

            return View(category);
        }
    }
}
