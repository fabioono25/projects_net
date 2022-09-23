using System;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DatabasePrePopulation
    {
        public static void PrePopulation(IApplicationBuilder app, bool isProd)
        {
            var serviceScope = app.ApplicationServices.CreateScope();

            Seed(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
        }

        private static void Seed(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("-- Applying migrations in production environment");

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }

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

