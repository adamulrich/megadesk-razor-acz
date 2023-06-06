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
    public class IndexModel : PageModel
    {
        private readonly MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext _context;

        public IndexModel(MegaDesk_Razor_ACZ.Data.MegaDesk_Razor_ACZContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote.ToListAsync();
            }
        }
    }
}
