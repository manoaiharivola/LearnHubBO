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
using System.Diagnostics;

namespace LearnHubBO.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(LearnHubBackOffice.Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Cours Cours { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

            if (!formateurId.HasValue)
            {
                TempData["ErrorMessage"] = "Vous devez �tre connect� pour acc�der � cette page.";
                return RedirectToPage("/Formateurs/Login");
            }

            string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
            ViewData["FormateurNom"] = formateurNom;

            if (id == null)
            {
                return NotFound();
            }

            var cours =  await _context.Courses.FirstOrDefaultAsync(m => m.IdCours == id);
            if (cours == null)
            {
                return NotFound();
            }
            Cours = cours;

           ViewData["IdCoursCategorie"] = new SelectList(_context.CoursCategories, "IdCoursCategorie", "NomCoursCategorie");
           ViewData["IdFormateur"] = new SelectList(_context.Formateurs, "IdFormateur", "NomFormateur");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var existingCours = await _context.Courses
                                               .AsNoTracking() // Ignorer le suivi pour �viter la modification accidentelle
                                               .FirstOrDefaultAsync(c => c.IdCours == Cours.IdCours);

            existingCours.TitreCours = Cours.TitreCours;

            int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

            Cours.IdFormateur = (int)formateurId;

            existingCours.IdFormateur = Cours.IdFormateur;
            existingCours.IdCoursCategorie = Cours.IdCoursCategorie;
            existingCours.Description = Cours.Description;

            existingCours.DateModificationCours = DateTime.Now;

            _context.Attach(existingCours).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursExists(Cours.IdCours))
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

        private bool CoursExists(int id)
        {
            return _context.Courses.Any(e => e.IdCours == id);
        }
    }
}
