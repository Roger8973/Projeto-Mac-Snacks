using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Context;
namespace LanchesMac.Repositories
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categories; 
    }
}
