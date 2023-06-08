using MegaDesk_Razor_ACZ.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace MegaDesk_Razor_ACZ.Models
{
    public class SeedData
    {
        Random rnd = new Random();


        public static void Initialize(IServiceProvider serviceProvider)
        {

            Random rnd = new Random();

            using (var context = new MegaDesk_Razor_ACZContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MegaDesk_Razor_ACZContext>>()))
            {
                // Look for any materials
                if (!context.Material.Any())
                {
                    context.Material.AddRange(
                        new Material { Name = "Rosewood", BasePrice = 300 },
                        new Material { Name = "Oak", BasePrice = 200 },
                        new Material { Name = "Veneer", BasePrice = 125 },
                        new Material { Name = "Laminate", BasePrice = 100 },
                        new Material { Name = "Pine", BasePrice = 50 }
                    );
                }

                // Look for any production speed cost data
                if (!context.ProductionSpeedCost.Any())
                {
                    context.ProductionSpeedCost.AddRange(
                        new ProductionSpeedCost { Description = "Normal", TierAPrice = 0, TierBPrice = 0, TierCPrice = 0 },
                        new ProductionSpeedCost { Description = "Rush 7 Days", TierAPrice = 30, TierBPrice = 35, TierCPrice = 40 },
                        new ProductionSpeedCost { Description = "Rush 5 Days", TierAPrice = 40, TierBPrice = 50, TierCPrice = 60 },
                        new ProductionSpeedCost { Description = "Rush 3 Days", TierAPrice = 60, TierBPrice = 70, TierCPrice = 80 }
                    );
                    context.SaveChanges();
                }

                // Look for any desks
                if (!context.Desk.Any())
                {
                    var Materials = (from m in context.Material select m).ToList();
                    int count = Materials.Count();

                    for (int i = 0; i < 10; i++)
                    {
                        int index = rnd.Next(0, count - 1);
                        context.Desk.Add(
                            new Desk
                            {
                                Depth = rnd.Next(12, 48),
                                Width = rnd.Next(24, 96),
                                DrawerCount = rnd.Next(1, 7),
                                MaterialId = Materials.ElementAt(index).Id,
                                Material = Materials.ElementAt(index)
                            }
                        );
                    }
                    context.SaveChanges();
                }

                // Look for quotes
                if (!context.DeskQuote.Any())
                {
                    
                    var ProdSpeeds = (from m in context.ProductionSpeedCost select m).ToList();
                    
                    int ProdSpeedCount = ProdSpeeds.Count();
                    int speedIndex = rnd.Next(ProdSpeedCount);

                    var Desks = (from d in context.Desk select d).ToList();

                    for (int i = 0; i < Desks.Count; i++)
                    {
                        DeskQuote deskQuote = new DeskQuote();
                        deskQuote.CustomerName = getRandomName();
                        deskQuote.ProductionSpeedCostId = ProdSpeeds.ElementAt(speedIndex).Id;
                        deskQuote.ProductionSpeedCost = ProdSpeeds.ElementAt(speedIndex);
                        deskQuote.Date = DateTime.Now;
                        deskQuote.DeskId = Desks[i].Id;
                        deskQuote.Desk = Desks[i];

                        var Materials = (from m in context.Material where m.Id == deskQuote.Desk.MaterialId select m).ToList();
                        deskQuote.Desk.Material = Materials[0];
                       
                        deskQuote.calculatePrice();

                        context.DeskQuote.Add(deskQuote); 
                    }
                    context.SaveChanges();
                }
            }

        }
        static string getRandomName()
        {
            Random rnd = new Random(); 
            
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(RandomString(rnd.Next(3, 10))) + " " + textInfo.ToTitleCase(RandomString(rnd.Next(3, 10)));
        }

        static string RandomString(int length)
        {
            Random rnd = new Random();

            const string chars = "aaaaabcdeeeeeeeefghiiiiijklllmnnnooooopqrrrssstttuuuuuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

    }

}



