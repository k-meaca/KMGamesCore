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
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<PlayerType> PlayerTypes { get; set; }

        public DbSet<PlayerGame> PlayersGames { get; set; }

        //----------METHODS----------//

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<GameCategory>().HasNoKey();
            //builder.Entity<PlayerGame>().HasNoKey();

            //builder.Entity<Game>()
            //    .HasMany(g => g.Categories)
            //    .WithMany(c => c.Games)
            //    .UsingEntity<GameCategory>(
            //        l => l.HasOne<Category>().WithMany(c => c.GameCategories),
            //        r => r.HasOne<Game>().WithMany(g => g.GameCategories)
            //    );

            //builder.Entity<Game>()
            //    .HasMany(g => g.PlayerTypes)
            //    .WithMany(p => p.Games)
            //    .UsingEntity<PlayerGame>(
            //        r => r.HasOne<PlayerType>().WithMany(p => p.PlayersGames),
            //        l => l.HasOne<Game>().WithMany(g=> g.PlayersGame)
            //    );

            builder.Entity<GameCategory>().HasKey(gc => new { gc.CategoryId, gc.GameId });

            //builder.Entity<GameCategory>().HasOne(gc => gc.Category)
            //                .WithMany(b => b.GameCategories);
            //.HasForeignKey(gc => gc.CategoryId);

            //builder.Entity<GameCategory>().HasOne(gc => gc.Game)
            //                .WithMany(g => g.GameCategories);
            //.HasForeignKey(gc => gc.GameId);

            builder.Entity<PlayerGame>().HasKey(pg => new { pg.GameId, pg.PlayerTypeId });

            //builder.Entity<PlayerGame>().HasOne(pg => pg.Game)
            //                .WithMany(g => g.PlayersGame)
            //                .HasForeignKey(pg => pg.GameId);

            //builder.Entity<PlayerGame>().HasOne(pg => pg.PlayerType)
            //                .WithMany(p => p.PlayersGames)
            //                .HasForeignKey(pg => pg.PlayerTypeId);
        }

    }
}
