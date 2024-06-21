using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using System.Diagnostics;

namespace LearnHubBO.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(LearnHubBackOffice.Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

            if (!formateurId.HasValue)
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour accéder à cette page.";
                return RedirectToPage("/Formateurs/Login");
            }

            string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
            ViewData["FormateurNom"] = formateurNom;
            ViewData["IdCoursCategorie"] = new SelectList(_context.CoursCategories, "IdCoursCategorie", "NomCoursCategorie");
            ViewData["IdFormateur"] = new SelectList(_context.Formateurs, "IdFormateur", "NomFormateur");
            return Page();
        }

        [BindProperty]
        public Cours Cours { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var now = DateTime.Now;
            Cours.DateCreationCours = now;
            Cours.DateModificationCours = now;

            var formateur = await _context.Formateurs.FindAsync(Cours.IdFormateur);

            if (formateur == null)
            {
                ModelState.AddModelError("Cours.IdFormateur", "Formateur non trouvé.");
                return Page();
            }

            Cours.Formateur = formateur;

            var categorie = await _context.CoursCategories.FindAsync(Cours.IdCoursCategorie);

            if (categorie == null)
            {
                ModelState.AddModelError("Cours.IdCoursCategorie", "Cours Categorie non trouvée.");
                return Page();
            }

            Cours.CoursCategorie = categorie;

            _context.Courses.Add(Cours);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
