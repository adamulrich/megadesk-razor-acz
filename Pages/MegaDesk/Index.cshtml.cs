using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor_ACZ.Data;
using MegaDesk_Razor_ACZ.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

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
        public IEnumerable<Material> Material { get; set; } = default!;
        public IEnumerable<ProductionSpeedCost> ProductionSpeedCost { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string SortType { get; set; }

        public List<SelectListItem> SortTypeList = new List<SelectListItem>() {
            new SelectListItem
            {
                Text = "By Date", Value = "1"
            },
            new SelectListItem
            {
                Text = "By Customer", Value = "2"
            }
         };

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        [BindProperty(SupportsGet = true)]
        public string MaterialName { get; set; }

        public SelectList MaterialList { get; set; }

        public int MaterialId { get; set; }

        public async Task OnGetAsync()
        {
            //// load the main data
            //if (_context.DeskQuote != null)
            //{
            //    DeskQuote = await _context.DeskQuote
            //    .Include(q => q.ProductionSpeedCost)
            //    .Include(q => q.Desk)
            //    .Include(q => q.Desk.Material)
            //    .ToListAsync();
            //}

            // load material values
            IQueryable<string> materialQuery = from m in _context.Material
                                           orderby m.Id
                                           select m.Name;

            MaterialList = new SelectList(await materialQuery.ToListAsync());

            var deskQuotes = from dq in _context.DeskQuote select dq;

            if (!string.IsNullOrEmpty(SearchString))
            {
                deskQuotes = deskQuotes.Where(dq => dq.CustomerName.ToLower().Contains(SearchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(MaterialName))
            {
                var materials = from m in _context.Material
                           where m.Name == MaterialName
                           select m.Id;
                MaterialId = materials.FirstOrDefault();
                deskQuotes = deskQuotes.Where(dq => dq.Desk.MaterialId == MaterialId);

            }
            if (SortType == "2")
            {
                deskQuotes = deskQuotes.OrderBy(dq => dq.CustomerName.ToLower());
            }
            else
            {
                deskQuotes = deskQuotes.OrderBy(dq => dq.Date);
            }

            // load the main data
            if (_context.DeskQuote != null)
            {
                DeskQuote = await deskQuotes
                .Include(q => q.ProductionSpeedCost)
                .Include(q => q.Desk)
                .Include(q => q.Desk.Material)
                .ToListAsync();
            }

        }
    }
}
