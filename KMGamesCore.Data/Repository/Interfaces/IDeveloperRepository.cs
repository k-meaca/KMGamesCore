﻿using KMGamesCore.Models.Models;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        bool Exist(Developer developer);

        bool Exist(int id);

        IEnumerable<Developer> GetDevelopers();

        bool ItsRelated(Developer developer);
        void Update(Developer developer);

    }
}
