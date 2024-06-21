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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(LearnHubBackOffice.Data.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

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
    }
}
