﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDesk_Razor_ACZ.Data;
using MegaDesk_Razor_ACZ.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaDesk_Razor_ACZ.Pages.MegaDesk
{
    public class CreateModel : PageModel
    {
        private readonly MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext _context;

        public CreateModel(MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            IQueryable<string> materialQuery = from m in _context.Material
                                               orderby m.Id
                                               select m.Name;

            MaterialList = new SelectList(materialQuery.ToList());

            IQueryable<string> rushOrderQuery = from m in _context.ProductionSpeedCost
                                                orderby m.Id
                                               select m.Description;

            RushOrderList = new SelectList(rushOrderQuery.ToList());

            return Page();
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;
        public Desk Desk { get; set; } = default!;
        public IEnumerable<Material> Material { get; set; } = default!;
        public SelectList MaterialList { get; set; } = default!;
        public IEnumerable<ProductionSpeedCost> ProductionSpeedCost { get; set; } = default!;
        public SelectList RushOrderList { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.DeskQuote == null || DeskQuote == null)
            {
                return Page();
            }

            

            var mat = await _context.Material.FirstOrDefaultAsync(q => q.Name == DeskQuote.Desk.Material.Name);
            var speed = await _context.ProductionSpeedCost.FirstOrDefaultAsync(q => q.Description == DeskQuote.ProductionSpeedCost.Description);


            DeskQuote.Desk.Material = mat;
            DeskQuote.ProductionSpeedCost = speed;

            DeskQuote.calculatePrice();
            DeskQuote.Date = DateTime.Now;

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
