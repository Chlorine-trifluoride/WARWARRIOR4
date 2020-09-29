using LeaderboardModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardAPI.Contexts
{
    public class LeaderboardContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Level> Levels { get; set; }

        public LeaderboardContext(DbContextOptions<LeaderboardContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
            modelBuilder.Entity<Score>().ToTable("scores").Property(p => p.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Player>().ToTable("players").Property(p => p.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Level>().ToTable("levels");
        }
    }
}
