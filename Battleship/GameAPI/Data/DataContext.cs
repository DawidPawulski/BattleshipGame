using GameAPI.Models;
using GameAPI.Models.Ships;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Player> Players { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Ship> Ships { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Battleship>().HasBaseType<Ship>();
            modelBuilder.Entity<Carrier>().HasBaseType<Ship>();
            modelBuilder.Entity<Cruiser>().HasBaseType<Ship>();
            modelBuilder.Entity<Submarine>().HasBaseType<Ship>();
            modelBuilder.Entity<Destroyer>().HasBaseType<Ship>();
            modelBuilder.Entity<PatrolBoat>().HasBaseType<Ship>();
            modelBuilder.Entity<TacticalBoat>().HasBaseType<Ship>();
            modelBuilder.Entity<Player>()
                .HasOne(c => c.Board)
                .WithOne(c => c.Player);
        }
    }
}