using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Context;
namespace LanchesMac.Repositories
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) // injetando uma instancia chamadada context (feita pelo container D.I) e atribuindo a variavel _context
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categories; // retornando do banco de dados uma coleção de categorias da tabela categoria
    }
}
