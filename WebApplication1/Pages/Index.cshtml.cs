using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        //This variable will fetch data from database 
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)   {  _db = db;  }

        public IList<Customer> Customers { get; private set; }

        public async Task OnGetAsync()
        {
            Customers = await _db.Customers.AsNoTracking().ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Get the cutomer from the database using ID
            var customer = await _db.Customers.FindAsync(id);

            if (customer != null)
            {
                //remove the select cutomer from database using id
                _db.Customers.Remove(customer);
                // save the changes done in In-Memory database 
                await _db.SaveChangesAsync();
            }
            // After the changes it will redirect to same pages.
            return RedirectToPage();
        }
    }
}
