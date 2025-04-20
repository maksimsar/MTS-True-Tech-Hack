using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend.Models;
using MTSTrueTechHack.Data;

namespace MTSTrueTechHack.Backend;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // миграции (на всякий случай)
        await db.Database.MigrateAsync();

        // демо‑user с ID = 1 (если его ещё нет)
        if (!await db.Users.AnyAsync(u => u.ID == 1))
        {
            db.Users.Add(new User {
                ID           = 1,               // тот самый FK
                Username     = "demo",
                Email        = "demo@example.com",
                PasswordHash = "NA",
                CreatedAt    = DateTime.UtcNow
            });

            await db.SaveChangesAsync();
            Console.WriteLine("🟢 Demo‑user #1 seeded");
        }
    }
}
