using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        // Stores temporary data which can be used in the subsequent request. 
        // TempData will be cleared out after the completion of a subsequent request.
        // TempData can be used to store only one time messages like error messages, validation messages.
        [TempData]
        // Message of string type and it will part of PageModel,
        // which is kind of like ViewModel
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Customers.Add(Customer);
            // This is means don't wait
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}