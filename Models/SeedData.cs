using MegaDesk_Razor_ACZ.Data;
using Microsoft.EntityFrameworkCore;

namespace MegaDesk_Razor_ACZ.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MegaDesk_Razor_ACZContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MegaDesk_Razor_ACZContext>>()))
            {
                // Look for any materials
                if (context.Material.Any())
                {
                    return;   // DB has been seeded
                }

                context.Material.AddRange(
                    new Material { Name = "Rosewood", BasePrice = 300},
                    new Material { Name = "Oak", BasePrice = 200 },
                    new Material { Name = "Veneer", BasePrice = 125 },
                    new Material { Name = "Laminate", BasePrice = 100 },
                    new Material { Name = "Pine", BasePrice = 50 }

                );

                // Look for any materials
                if (context.ProductionSpeedCost.Any())
                {
                    return;   // DB has been seeded
                }

                context.ProductionSpeedCost.AddRange(
                    new ProductionSpeedCost { Description = "Normal", TierAPrice = 0, TierBPrice = 0, TierCPrice = 0 },
                    new ProductionSpeedCost { Description = "Rush 7 Days", TierAPrice = 30, TierBPrice = 35, TierCPrice = 40},
                    new ProductionSpeedCost { Description = "Rush 5 Days", TierAPrice = 40, TierBPrice = 50, TierCPrice = 60 },
                    new ProductionSpeedCost { Description = "Rush 3 Days", TierAPrice = 60, TierBPrice = 70, TierCPrice = 80 }
                );

                context.SaveChanges();
            }
        }
    }
}



