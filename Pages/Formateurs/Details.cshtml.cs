using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;

namespace LearnHubBO.Pages.Formateurs
{
    public class DetailsModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public DetailsModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        public Formateur Formateur { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateurs.FirstOrDefaultAsync(m => m.IdFormateur == id);
            if (formateur == null)
            {
                return NotFound();
            }
            else
            {
                Formateur = formateur;
            }
            return Page();
        }
    }
}
