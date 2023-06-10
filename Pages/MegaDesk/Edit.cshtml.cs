using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor_ACZ.Data;
using MegaDesk_Razor_ACZ.Models;

namespace MegaDesk_Razor_ACZ.Pages.MegaDesk
{
    public class EditModel : PageModel
    {
        private readonly MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext _context;

        public EditModel(MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;

        public SelectList MaterialList { get; set; } = default!;
        public SelectList RushOrderList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            IQueryable<string> materialQuery = from m in _context.Material
                                               orderby m.Id
                                               select m.Name;

            MaterialList = new SelectList(materialQuery.ToList());

            IQueryable<string> rushOrderQuery = from m in _context.ProductionSpeedCost
                                                orderby m.Id
                                                select m.Description;

            RushOrderList = new SelectList(rushOrderQuery.ToList());

            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote
                .Include(q => q.ProductionSpeedCost)
                .Include(q => q.Desk)
                .Include(q => q.Desk.Material)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (deskquote == null)
            {
                return NotFound();
            }

            DeskQuote = deskquote;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var deskquote = await _context.DeskQuote
                .Include(q => q.ProductionSpeedCost)
                .Include(q => q.Desk)
                .Include(q => q.Desk.Material)
                .FirstOrDefaultAsync(q => q.Id == DeskQuote.Id);
            if (deskquote == null)
            {
                return NotFound();
            }

            var mat = await _context.Material.FirstOrDefaultAsync(q => q.Name == DeskQuote.Desk.Material.Name);
            var speed = await _context.ProductionSpeedCost.FirstOrDefaultAsync(q => q.Description == DeskQuote.ProductionSpeedCost.Description);


            deskquote.CustomerName = DeskQuote.CustomerName;
            deskquote.Desk.Width = DeskQuote.Desk.Width;
            deskquote.Desk.Depth = DeskQuote.Desk.Depth;
            deskquote.Desk.DrawerCount = DeskQuote.Desk.DrawerCount;
            deskquote.Desk.Material = mat;
            deskquote.ProductionSpeedCost = speed;
            deskquote.calculatePrice();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeskQuoteExists(int id)
        {
            return _context.DeskQuote.Any(e => e.Id == id);
        }
    }
}
