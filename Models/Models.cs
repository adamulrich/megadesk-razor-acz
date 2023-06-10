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
        [Range(24, 96)]
        [Required]
        [Display(Name = "Width")]
        public int Width { get; set; }
        [Range(12, 48)]
        [Required]
        [Display(Name = "Depth")]
        public int Depth { get; set; }
        [Range(0, 7)]
        [Required]
        [Display(Name = "Drawer Count")]
        public int DrawerCount { get; set; }

        [Required]
        [Display(Name = "Material Type")]
        public int MaterialId { get; set; }

        public Material? Material { get; set; }
    }

        public class Material
        {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Material Type")]
        public string? Name { get; set; }

        [Required]
        public int BasePrice { get; set; }
    }

    public class ProductionSpeedCost
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rush Time")]
        public string? Description { get; set; }

        public double TierAPrice { get; set; }
        public double TierBPrice { get; set; }
        public double TierCPrice { get; set; }

    }

    public class DeskQuote
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        public int ProductionSpeedCostId { get; set; }
        public ProductionSpeedCost? ProductionSpeedCost { get; set; }

        public double Price { get; set; }
        
        public int? DeskId { get; set; }
        public Desk Desk { get; set; }

        public DateTime Date { get; set; }

        public string DateforDisplay
        {
            get
            {
                return this.Date.ToString("dd MMM yy");
            }
        }

        public string PriceforDisplay
        {
            get
            {
                return this.Price.ToString("C");
            }
        }

        // constants
        int BASE_COST = 200;
        int SURFACE_AREA_COST_INCREASE_SIZE = 1000;
        int EXTRA_SURFACE_AREA_COST = 1;
        int DRAWER_COST = 50;

        public void calculatePrice()
        {
            if (this.Desk is null || this.Desk.Material is null || this.ProductionSpeedCost is null)
            {
                Console.WriteLine("invalid object, something is null.");
                return;
            }

            // calculate size of desk and drawers and material
            double deskPrice = BASE_COST +
                            (this.Desk.DrawerCount * DRAWER_COST) +
                            this.Desk.Material.BasePrice;
                            
            // if large, then add extra
            int deskSize = this.Desk.Depth * this.Desk.Width;
            deskPrice += Math.Max(deskSize - SURFACE_AREA_COST_INCREASE_SIZE, 0) * EXTRA_SURFACE_AREA_COST;

            // add production cost based on size
            // should resolve to 0, 1, 2
            int rushIndex = Math.Min((int)(deskSize / 1000), 2);

            switch (rushIndex)
            {
                case 0:
                    deskPrice += this.ProductionSpeedCost.TierAPrice;
                    break;
                case 1:
                    deskPrice += this.ProductionSpeedCost.TierBPrice;
                    break;
                default:
                    deskPrice += this.ProductionSpeedCost.TierCPrice;
                    break;
            }

            // set price
            this.Price = deskPrice;
        }




        }
    }