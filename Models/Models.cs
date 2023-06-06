using MegaDesk_Razor_ACZ.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaDesk_Razor_ACZ.Models
{
    public class Desk
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Width")]
        public int Width { get; set; }

        [Required]
        [Display(Name = "Depth")]
        public int Depth { get; set; }

        [Required]
        [Display(Name = "Drawer Count")]
        public int DrawerCount { get; set; }


        [Required]
        [Display(Name = "Material Type")]
        public int MaterialId { get; set; }

        public Material? material { get; set; }

    }

    public class Material
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Material Type")]
        public string? Name { get; set; }

        [Required]
        public int? BasePrice { get; set; }
    }

    public class ProductionSpeedCost
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rush Time")]
        public string? Description { get; set; }

        public double? TierAPrice { get; set; }
        public double? TierBPrice { get; set; }
        public double? TierCPrice { get; set; }

    }

    public class DeskQuote
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public ProductionSpeedCost? ProductionSpeedCost { get; set; }

        public double Price { get; set; }
        public Desk? Desk { get; set; }

        public DateTime? Date { get; set; }

        
    }
}