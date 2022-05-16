using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define as classes para mapear das tabelas 
        public DbSet<Category> Categories { get; set; } // Vai criar a tabela Categories
        public DbSet<Snack> Snacks { get; set; } // Vai criar a tabela Snacks
    }
}
