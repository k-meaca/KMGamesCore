﻿using KMGamesCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.Repository.Interfaces
{
    public interface IApplicationUsersRepository : IRepository<ApplicationUser>
    {
        void AddGamesTo(ApplicationUser user, List<PurchasedGame> games);
    }
}
