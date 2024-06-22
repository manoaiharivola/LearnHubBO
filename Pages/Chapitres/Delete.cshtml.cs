using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBO.Models;
using LearnHubBackOffice.Data;

namespace LearnHubBO.Pages.Chapitres
{
    public class DeleteModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public DeleteModel(LearnHubBackOffice.Data.AppDbContext context)
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

            var chapitre = await _context.Chapitres.FirstOrDefaultAsync(m => m.IdChapitre == id);

            if (chapitre == null)
            {
                return NotFound();
            }
            else
            {
                Chapitre = chapitre;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapitre = await _context.Chapitres.FindAsync(id);
            if (chapitre != null)
            {
                Chapitre = chapitre;
                _context.Chapitres.Remove(Chapitre);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
