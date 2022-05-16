using LanchesMac.Models;
namespace LanchesMac.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
