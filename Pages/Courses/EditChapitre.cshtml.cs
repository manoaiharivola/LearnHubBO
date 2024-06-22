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
using LearnHubBackOffice.Models;

namespace LearnHubBO.Pages.Courses
{
    public class EditChapitreModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditChapitreModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Chapitre Chapitre { get; set; } = default!;

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

            if (id == null)
            {
                return NotFound();
            }

            var chapitre = await _context.Chapitres.FirstOrDefaultAsync(m => m.IdChapitre == id);
            if (chapitre == null)
            {
                return NotFound();
            }
            Chapitre = chapitre;

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

            Cours = cours;

            if (Cours == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var existingChapitre = await _context.Chapitres
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(c => c.IdChapitre == Chapitre.IdChapitre);

            existingChapitre.IdCours = Chapitre.IdCours;

            var cours = await _context.Courses.FirstOrDefaultAsync(m => m.IdCours == existingChapitre.IdCours);

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

            existingChapitre.Cours = cours;
            existingChapitre.TitreChapitre = Chapitre.TitreChapitre;
            var ancienOrdre = existingChapitre.Ordre;
            existingChapitre.Ordre = Chapitre.Ordre;
            existingChapitre.DateModificationChapitre = DateTime.Now;
            existingChapitre.Contenu = Chapitre.Contenu;

            if (existingChapitre.TitreChapitre == null || existingChapitre.Contenu == null)
            {
                int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

                if (!formateurId.HasValue)
                {
                    TempData["ErrorMessage"] = "Vous devez être connecté pour accéder à cette page.";
                    return RedirectToPage("/Formateurs/Login");
                }

                string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
                ViewData["FormateurNom"] = formateurNom;

                Cours = existingChapitre.Cours;
                return Page();
            }

            _context.Attach(existingChapitre).State = EntityState.Modified;

            if(ancienOrdre < existingChapitre.Ordre)
            {
                var chapitres = await _context.Chapitres
                .Where(c => c.IdCours == Chapitre.IdCours && c.Ordre <= existingChapitre.Ordre && c.Ordre > ancienOrdre)
                .OrderBy(c => c.Ordre)
                .ToListAsync();

                foreach (var c in chapitres)
                {
                    if (c.IdChapitre != existingChapitre.IdChapitre)
                    {
                        c.Ordre--;
                    }
                }
            }
            else if (ancienOrdre > existingChapitre.Ordre)
            {
                var chapitres = await _context.Chapitres
                .Where(c => c.IdCours == Chapitre.IdCours && c.Ordre >= existingChapitre.Ordre)
                .OrderBy(c => c.Ordre)
                .ToListAsync();

                foreach (var c in chapitres)
                {
                    if (c.IdChapitre != existingChapitre.IdChapitre)
                    {
                        c.Ordre++;
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapitreExists(existingChapitre.IdChapitre))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Chapitres", new { id = existingChapitre.IdCours });
        }

        private bool ChapitreExists(int id)
        {
            return _context.Chapitres.Any(e => e.IdChapitre == id);
        }
    }
}
