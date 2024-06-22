using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LearnHubBO.Models;
using LearnHubBackOffice.Data;

namespace LearnHubBO.Pages.Chapitres
{
    public class CreateModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public CreateModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdCours"] = new SelectList(_context.Courses, "IdCours", "TitreCours");
            return Page();
        }

        [BindProperty]
        public Chapitre Chapitre { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chapitres.Add(Chapitre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
