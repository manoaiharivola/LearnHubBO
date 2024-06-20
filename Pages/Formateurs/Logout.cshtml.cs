using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnHubBO.Pages.Formateurs
{
    public class LogoutModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            HttpContext.Session.Remove("FormateurId");
            HttpContext.Session.Remove("FormateurNom");
            TempData["SuccessMessage"] = "Vous �tes d�connect�.";
            return RedirectToPage("/Formateurs/Login");
        }
    }
}
