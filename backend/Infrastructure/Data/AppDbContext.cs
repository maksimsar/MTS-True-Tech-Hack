using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace MTSTrueTechHack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Schema> Schemas { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        // Резервная конфигурация для миграций
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString); // Можно заменить на UseNpgsql для PostgreSQL
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка сущности User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.ID);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
                entity.Property(u => u.CreatedAt)
                    .IsRequired()
                    .HasColumnType("timestamp without time zone");
            });

            // Настройка сущности Schema
            modelBuilder.Entity<Schema>(entity =>
            {
                entity.ToTable("Schemas");

                entity.HasKey(s => s.ID);
                entity.Property(s => s.UserID).IsRequired();
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Description).HasMaxLength(500);
                entity.Property(s => s.JSONSchema).IsRequired();
                entity.Property(s => s.CreatedAt).IsRequired().HasColumnType("timestamp without time zone");
                entity.Property(s => s.UpdatedAt).IsRequired().HasColumnType("timestamp without time zone");

                entity.HasOne(s => s.User)
                    .WithMany(u => u.Schemas)
                    .HasForeignKey(s => s.UserID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка сущности Message
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");

                entity.HasKey(m => m.ID);
                entity.Property(m => m.SchemaID).IsRequired();
                entity.Property(m => m.Text).IsRequired().HasMaxLength(1000);
                entity.Property(m => m.IsFromUser).IsRequired();
                entity.Property(m => m.Timestamp).IsRequired().HasColumnType("timestamp without time zone");

                entity.HasOne(m => m.Schema)
                    .WithMany(s => s.Messages)
                    .HasForeignKey(m => m.SchemaID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}