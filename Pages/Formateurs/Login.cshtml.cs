using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHubBO.Pages.Formateurs
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "L'adresse email n'est pas valide.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
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

            var formateur = await _context.Formateurs.FirstOrDefaultAsync(f => f.Email == Email);

            if (formateur == null || !formateur.VerifyPassword(MotDePasse))
            {
                ModelState.AddModelError(string.Empty, "Les informations de connexion sont incorrectes.");
                return Page();
            }

            HttpContext.Session.SetInt32("UserFormateur", formateur.IdFormateur);

            return RedirectToPage("/Index");
        }
    }
}
