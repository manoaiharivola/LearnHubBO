using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LearnHubBO.Pages.Formateurs
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(AppDbContext context, ILogger<RegisterModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string NomFormateur { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "L'adresse email n'est pas valide.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Le mot de passe doit contenir au moins {2} caractères.", MinimumLength = 8)]
        public string MotDePasse { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var formateur = new Formateur
                {
                    NomFormateur = NomFormateur,
                    Email = Email
                };

                formateur.SetPassword(MotDePasse); 

                _context.Formateurs.Add(formateur);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'inscription d'un formateur.");
                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de l'inscription. Veuillez réessayer plus tard.");
                return Page();
            }
        }
    }
}