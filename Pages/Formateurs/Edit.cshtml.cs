using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace LearnHubBO.Pages.Formateurs
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EditModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(AppDbContext context, ILogger<EditModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public int IdFormateur { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Le nom est requis.")]
        public string NomFormateur { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "L'adresse email n'est pas valide.")]
        public string Email { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Le mot de passe doit contenir au moins {2} caract�res.", MinimumLength = 8)]
        public string? MotDePasse { get; set; }

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

            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null)
            {
                return NotFound();
            }

            IdFormateur = formateur.IdFormateur;
            NomFormateur = formateur.NomFormateur;
            Email = formateur.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");

                if (!formateurId.HasValue)
                {
                    TempData["ErrorMessage"] = "Vous devez �tre connect� pour acc�der � cette page.";
                    return RedirectToPage("/Formateurs/Login");
                }

                string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
                ViewData["FormateurNom"] = formateurNom;

                return Page();
            }

            var formateur = await _context.Formateurs.FindAsync(IdFormateur);
            if (formateur == null)
            {
                return NotFound();
            }

            try
            {
                var utilisateurExistant = await _context.Formateurs
                    .Where(f => f.Email == Email && f.IdFormateur != IdFormateur)
                    .FirstOrDefaultAsync();

                int? formateurId = _httpContextAccessor.HttpContext.Session.GetInt32("FormateurId");


                if (utilisateurExistant != null)
                {
                    if (!formateurId.HasValue)
                    {
                        TempData["ErrorMessage"] = "Vous devez �tre connect� pour acc�der � cette page.";
                        return RedirectToPage("/Formateurs/Login");
                    }

                    string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
                    ViewData["FormateurNom"] = formateurNom;

                    ModelState.AddModelError("Email", "Un utilisateur avec cette adresse email existe d�j�.");
                    return Page();
                }

                
                formateur.NomFormateur = NomFormateur;
                if(formateur.IdFormateur == formateurId)
                {
                    HttpContext.Session.SetString("FormateurNom", formateur.NomFormateur);
                }
                formateur.Email = Email;

                if (!string.IsNullOrEmpty(MotDePasse))
                {
                    formateur.SetPassword(MotDePasse);
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Les informations du formateur ont �t� mises � jour avec succ�s.";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise � jour du formateur.");
                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la mise � jour. Veuillez r�essayer plus tard.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise � jour du formateur.");
                ModelState.AddModelError(string.Empty, "Une erreur est survenue. Veuillez r�essayer plus tard.");
                return Page();
            }
        }
    }
}
