using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorDropDown.Data;
using RazorDropDown.Domain;

namespace RazorDropDown.Pages.Store
{
    public class EditModel : PageModel
    {
        private readonly RazorDropDown.Data.OurDbContext _context;

        public EditModel(RazorDropDown.Data.OurDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shop Shop { get; set; } = default!;
       
        /// <summary>
        /// Get the object that want to modify
        /// Get any other data I need to display on the form
        /// Invoke the rendering of the edit form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            /// if no id retrun a http 404 code
            if (id == null) { return NotFound();}
            // get the shop out of the database
            var shop =  await _context.Shops.FirstOrDefaultAsync(m => m.StoreId == id);
            // if the shop does not exist return a http 404 code
            if (shop == null) { return NotFound();}
            // assign this shop to the model property Shop
            Shop = shop;
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "Description");
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            // check if the model is valid 
            if (!ModelState.IsValid){ return Page();}
            // that the state of the shop you have provided had changed
            _context.Attach(Shop).State = EntityState.Modified;
            try
            {
                // execute an Update set.... sql
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(Shop.StoreId))
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

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.StoreId == id);
        }
    }
}
