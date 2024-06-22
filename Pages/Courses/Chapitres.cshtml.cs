using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using LearnHubBO.Models;

namespace LearnHubBO.Pages.Courses
{
    public class ChapitresModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChapitresModel(LearnHubBackOffice.Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Chapitre> Chapitres { get; set; } = default!;
        [BindProperty]
        public Cours Cours { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

            if (!formateurId.HasValue)
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour accéder à cette page.";
                return RedirectToPage("/Formateurs/Login");
            }

            string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
            ViewData["FormateurNom"] = formateurNom;

            var cours = await _context.Courses.FirstOrDefaultAsync(m => m.IdCours == id);

            if (cours == null)
            {
                return NotFound();
            }
            
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

            Cours = cours;

            Chapitres = await _context.Chapitres
                .Where(c => c.IdCours == id)
                .OrderBy(ch => ch.Ordre)
                .ToListAsync();

            return Page();
        }
    }
}