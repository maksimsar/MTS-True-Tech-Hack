using Microsoft.EntityFrameworkCore;
using MTSTrueTechHack.Backend.Models;
using MTSTrueTechHack.Data;

namespace MTSTrueTechHack.Backend;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!await db.Users.AnyAsync())
        {
            db.Users.Add(new User {
                ID           = 1,
                Username     = "demo",
                Email        = "demo@example.com",
                PasswordHash = "NA",
                CreatedAt    = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }
    }
}
