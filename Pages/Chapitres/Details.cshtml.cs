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
    public class DetailsModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public DetailsModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
