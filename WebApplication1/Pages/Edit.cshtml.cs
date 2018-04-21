using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db) { _db = db; }

        // By default the model binding logic maps the form field names and the property names.
        [BindProperty]
        // Customer with Auto-Implemented Properties
        public Customer Customer { get; set; }
        // In an asynchronous process, the application can continue with other work 
        // that doesn't depend on the web resource until the potentially blocking task finishes.
        // OnGetAsyn method loads customer detail to edit page
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // the method can't continue until the awaited asynchronous operation is complete.
            Customer = await _db.Customers.FindAsync(id);
            // If no customer record found, return to the index page
            if (Customer == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        // This method post the changes made in the edit page 
        // and return back to index page
        // using Async Task so that page does not get blocked
        public async Task<IActionResult> OnPostAsync()
        {
            // Return to the same page if modelstate is not valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Customer {Customer.Id} not found!", e);
            }

            return RedirectToPage("/Index");
        }
    }
}