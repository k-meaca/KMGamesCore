﻿using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        bool Exist(Country country);

        bool Exist(int id);

        void Update(Country country);
    }
}
