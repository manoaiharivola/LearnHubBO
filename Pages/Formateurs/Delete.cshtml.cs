using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using Microsoft.AspNetCore.Http;

namespace LearnHubBO.Pages.Formateurs
{
    public class DeleteModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(LearnHubBackOffice.Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        [BindProperty]
        public Formateur Formateur { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null)
            {
                return NotFound();
            }

            var idUtilisateurConnecte = HttpContext.Session.GetInt32("FormateurId");
            if (idUtilisateurConnecte == formateur.IdFormateur)
            {
                int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

                if (!formateurId.HasValue)
                {
                    TempData["ErrorMessage"] = "Vous devez être connecté pour accéder à cette page.";
                    return RedirectToPage("/Formateurs/Login");
                }

                string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
                ViewData["FormateurNom"] = formateurNom;

                TempData["ErrorMessage"] = "Vous ne pouvez pas supprimer votre propre compte.";
                if (id == null)
                {
                    return NotFound();
                }

                var formateurError = await _context.Formateurs.FirstOrDefaultAsync(m => m.IdFormateur == id);

                if (formateurError == null)
                {
                    return NotFound();
                }
                else
                {
                    Formateur = formateurError;
                }
                return Page();
            }

            _context.Formateurs.Remove(formateur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
