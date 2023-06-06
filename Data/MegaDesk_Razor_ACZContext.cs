using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor_ACZ.Models;

namespace MegaDesk_Razor_ACZ.Data
{
    public class MegaDesk_Razor_ACZContext : DbContext
    {
        public MegaDesk_Razor_ACZContext (DbContextOptions<MegaDesk_Razor_ACZContext> options)
            : base(options)
        {

        }

        public DbSet<MegaDesk_Razor_ACZ.Models.DeskQuote> DeskQuote { get; set; } = default!;
        public DbSet<MegaDesk_Razor_ACZ.Models.Desk> Desk { get; set; } = default!;
        public DbSet<MegaDesk_Razor_ACZ.Models.Material> Material { get; set; } = default!;
        public DbSet<MegaDesk_Razor_ACZ.Models.ProductionSpeedCost> ProductionSpeedCost { get; set; } = default!;   
    }
}
