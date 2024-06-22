using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnHubBO.Models;
using LearnHubBackOffice.Data;

namespace LearnHubBO.Pages.Chapitres
{
    public class EditModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public EditModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chapitre Chapitre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapitre =  await _context.Chapitres.FirstOrDefaultAsync(m => m.IdChapitre == id);
            if (chapitre == null)
            {
                return NotFound();
            }
            Chapitre = chapitre;
           ViewData["IdCours"] = new SelectList(_context.Courses, "IdCours", "TitreCours");
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

            _context.Attach(Chapitre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapitreExists(Chapitre.IdChapitre))
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

        private bool ChapitreExists(int id)
        {
            return _context.Chapitres.Any(e => e.IdChapitre == id);
        }
    }
}
