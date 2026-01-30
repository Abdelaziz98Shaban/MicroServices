using PlatformService.Models;

namespace PlatformService.Data;

public static class SeedData
{
    public static void PopulateData(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedPlatforms(dbContext);
        }
    }

    private static void SeedPlatforms(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("Seeding data...");

            var platforms = new List<Platform>
            {
                new Platform { Id = 1, Name = "Windows", Publisher = "Microsoft", Cost = "Free" },
                new Platform { Id = 2, Name = "Linux", Publisher = "Open Source", Cost = "Free" },
                new Platform { Id = 3, Name = "macOS", Publisher = "Apple", Cost = "$199" }
            };

            context.Platforms.AddRange(platforms);
            context.SaveChanges();

            Console.WriteLine("Data seeding completed successfully!");
        }
    }
}
