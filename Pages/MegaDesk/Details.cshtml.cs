using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor_ACZ.Data;
using MegaDesk_Razor_ACZ.Models;

namespace MegaDesk_Razor_ACZ.Pages.MegaDesk
{
    public class DetailsModel : PageModel
    {
        private readonly MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext _context;

        public DetailsModel(MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext context)
        {
            _context = context;
        }

      public DeskQuote DeskQuote { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote
                .Include(q => q.ProductionSpeedCost)
                .Include(q => q.Desk)
                .Include(q => q.Desk.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deskquote == null)
            {
                return NotFound();
            }
            else 
            {
                DeskQuote = deskquote;
            }
            return Page();
        }
    }
}
