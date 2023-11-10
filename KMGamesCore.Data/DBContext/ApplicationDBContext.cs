using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Data.DBContext
{
    public class ApplicationDBContext : IdentityDbContext
    {

        //----------CONSTRUCTOR----------//

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        
        //----------DBSETS----------//

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerType> PlayerTypes { get; set; }

    }
}
