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

        public EditModel(AppDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
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
        [StringLength(100, ErrorMessage = "Le mot de passe doit contenir au moins {2} caractères.", MinimumLength = 8)]
        public string? MotDePasse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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

            IdFormateur = formateur.IdFormateur;
            NomFormateur = formateur.NomFormateur;
            Email = formateur.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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

                if (utilisateurExistant != null)
                {
                    ModelState.AddModelError("Email", "Un utilisateur avec cette adresse email existe déjà.");
                    return Page();
                }

                formateur.NomFormateur = NomFormateur;
                formateur.Email = Email;

                if (!string.IsNullOrEmpty(MotDePasse))
                {
                    formateur.SetPassword(MotDePasse);
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Les informations du formateur ont été mises à jour avec succès.";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du formateur.");
                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la mise à jour. Veuillez réessayer plus tard.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du formateur.");
                ModelState.AddModelError(string.Empty, "Une erreur est survenue. Veuillez réessayer plus tard.");
                return Page();
            }
        }
    }
}
