using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnHubBackOffice.Data;
using LearnHubBO.Models;
using LearnHubBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnHubBO.Pages.Courses
{
    public class CreateChapitreModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateChapitreModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Chapitre Chapitre { get; set; } = default!;

        [BindProperty]
        public Cours Cours { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? idCours)
        {
            int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

            if (!formateurId.HasValue)
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour accéder à cette page.";
                return RedirectToPage("/Formateurs/Login");
            }

            string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
            ViewData["FormateurNom"] = formateurNom;

            var cours = await _context.Courses.FirstOrDefaultAsync(m => m.IdCours == idCours);

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

            if (Cours == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cours = await _context.Courses.FirstOrDefaultAsync(m => m.IdCours == Chapitre.IdCours);

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

            Chapitre.Cours = cours;
            var now = DateTime.Now;
            Chapitre.DateCreationChapitre = now;
            Chapitre.DateModificationChapitre = now;

            if (Chapitre.TitreChapitre == null)
            {
                int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

                if (!formateurId.HasValue)
                {
                    TempData["ErrorMessage"] = "Vous devez être connecté pour accéder à cette page.";
                    return RedirectToPage("/Formateurs/Login");
                }

                string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
                ViewData["FormateurNom"] = formateurNom;
                return Page();
            }

            var existingChapitres = await _context.Chapitres
                .Where(c => c.IdCours == Chapitre.IdCours && c.Ordre >= Chapitre.Ordre)
                .OrderBy(c => c.Ordre)
                .ToListAsync();

            foreach (var existingChapitre in existingChapitres)
            {
                existingChapitre.Ordre++;
            }

            _context.Chapitres.Add(Chapitre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Chapitres", new { id = Chapitre.IdCours });
        }
    }
}
