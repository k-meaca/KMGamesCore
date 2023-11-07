using KMGamesCore.Models.Models;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        bool Exist(Developer developer);

        bool Exist(int id);

        IEnumerable<Developer> GetDevelopers();

        void Update(Developer developer);

    }
}
