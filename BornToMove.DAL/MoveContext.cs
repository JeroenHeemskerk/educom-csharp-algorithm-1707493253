using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL {
    public class MoveContext : DbContext {

        public DbSet<Move> Move { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            builder.UseSqlServer(connectionString);
            base.OnConfiguring(builder);
        }
    }
}