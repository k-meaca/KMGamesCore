﻿using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        bool Exist(City city);

        bool Exist(int id);

        void Update(City city);
    }
}
