using System;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DatabasePrePopulation
    {
        public static void PrePopulation(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();

            Seed(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void Seed(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data");

                context.Platforms.AddRange(
                    new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" },
                    new Platform { Name = "Docker", Publisher = "Docker, Inc.", Cost = "Free" }
                );

                context.SaveChanges();

            } else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}

