using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Models;

namespace MTSTrueTechHack.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Schema> Schemas { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schema>()
                .HasOne(s => s.User)
                .WithMany(u => u.Schemas)
                .HasForeignKey(s => s.UserID);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Schema)
                .WithMany(s => s.Messages)
                .HasForeignKey(m => m.SchemaID);
        }
    }
}