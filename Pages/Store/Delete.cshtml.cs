using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorDropDown.Data;
using RazorDropDown.Domain;

namespace RazorDropDown.Pages.Store
{
    public class DeleteModel : PageModel
    {
        private readonly RazorDropDown.Data.OurDbContext _context;

        public DeleteModel(RazorDropDown.Data.OurDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shop Shop { get; set; } = default!;

        /// <summary>
        /// Display the object i am considering deleting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // does id exist?
            if (id == null)
            {
                return NotFound();
            }
            // get the shop with that Id
            var shop = await _context.Shops.FirstOrDefaultAsync(m => m.StoreId == id);

            // if no shop exists then return 404
            if (shop == null)
            {
                return NotFound();
            }
            else
            {  // assign this shop to the bindpr
                Shop = shop;
            }
            return Page();
        }

        /// <summary>
        /// Grab that object
        /// Remove it from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // this id exists
            if (id == null)
            {
                return NotFound();
            }
            // get the shop with that id.
            var shop = await _context.Shops.FindAsync(id);
            if (shop != null)
            {
                  Shop = shop;
                _context.Shops.Remove(Shop);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
