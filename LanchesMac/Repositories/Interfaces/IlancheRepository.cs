using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces
{
    public interface IlancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }

        IEnumerable<Lanche> LanchesPreferidos { get; }

        Lanche GetLancheById(int id);
    }
}
