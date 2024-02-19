using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace BornToMove.DAL {
    public class MoveContext : DbContext {

        public DbSet<Move> Move { get; set; }
        public DbSet<MoveRating> MoveRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            //string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=born2move;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            builder.UseSqlServer(connectionString);
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MoveRating>()
            .Property<int>("Moveid");

            modelBuilder.Entity<Move>()
                .HasMany(m => m.Ratings)
                .WithOne(r => r.Move)
                .HasForeignKey("Moveid")
                .IsRequired(false);
        }
    }
}