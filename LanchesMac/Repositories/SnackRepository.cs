using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
namespace LanchesMac.Repositories
{
    public class SnackRepository : ISnacksRepository
    {
        private readonly AppDbContext _context;

        public SnackRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Snack> Snacks => _context.Snacks.Include(c => c.Category); //Incluido na consulta os lanches na categoria 

        public IEnumerable<Snack> FavoriteSnacks => _context.Snacks
            .Where(f => f.IsFavoriteSnack)
            .Include(c => c.Category);

        public Snack GetSnackById(int snackId)
        {
            return _context.Snacks.FirstOrDefault(i => i.SnackId == snackId);
        }
    }
}
