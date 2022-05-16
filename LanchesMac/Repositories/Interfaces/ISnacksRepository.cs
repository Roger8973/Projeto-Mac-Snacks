using LanchesMac.Models;
namespace LanchesMac.Repositories.Interfaces
{
    public interface ISnacksRepository
    {
        IEnumerable<Snack> Snacks { get; }
        IEnumerable<Snack> FavoriteSnacks { get; }
        Snack GetSnackById(int snackId);

    }
}
