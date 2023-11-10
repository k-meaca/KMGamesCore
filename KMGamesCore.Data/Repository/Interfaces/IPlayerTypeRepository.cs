﻿using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IPlayerTypeRepository : IRepository<PlayerType>
    {
        bool Exist(PlayerType type);

        bool Exist(int id);

        void Update(PlayerType type);
    }
}