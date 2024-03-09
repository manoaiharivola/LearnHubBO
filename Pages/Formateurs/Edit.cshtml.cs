using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;

namespace LearnHubBO.Pages.Formateurs
{
    public class EditModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public EditModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Formateur Formateur { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateur =  await _context.Formateurs.FirstOrDefaultAsync(m => m.IdFormateur == id);
            if (formateur == null)
            {
                return NotFound();
            }
            Formateur = formateur;
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

            _context.Attach(Formateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormateurExists(Formateur.IdFormateur))
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

        private bool FormateurExists(int id)
        {
            return _context.Formateurs.Any(e => e.IdFormateur == id);
        }
    }
}
