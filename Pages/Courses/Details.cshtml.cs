using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;

namespace LearnHubBO.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public DetailsModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        public Cours Cours { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Courses.FirstOrDefaultAsync(m => m.IdCours == id);

            var formateur = await _context.Formateurs.FindAsync(cours.IdFormateur);

            if (formateur == null)
            {
                ModelState.AddModelError("Cours.IdFormateur", "Formateur non trouvé.");
                return Page();
            }

            cours.Formateur = formateur;

            var categorie = await _context.CoursCategories.FindAsync(cours.IdCoursCategorie);

            if (categorie == null)
            {
                ModelState.AddModelError("Cours.IdCoursCategorie", "Cours Categorie non trouvée.");
                return Page();
            }

            cours.CoursCategorie = categorie;

            if (cours == null)
            {
                return NotFound();
            }
            else
            {
                Cours = cours;
            }
            return Page();
        }
    }
}
