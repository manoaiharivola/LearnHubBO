using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnHubBO.Pages.CoursCategories
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
                return RedirectToPage("../Formateurs/Login");
            }

            string formateurNom = _httpContextAccessor.HttpContext.Session.GetString("FormateurNom");
            ViewData["FormateurNom"] = formateurNom;

            return Page();
        }

        [BindProperty]
        public CoursCategorie CoursCategorie { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CoursCategories.Add(CoursCategorie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostImportAsync(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ModelState.AddModelError("csvFile", "Please select a CSV file.");
                return Page();
            }

            var categories = new List<CoursCategorie>();

            using (var stream = new StreamReader(csvFile.OpenReadStream()))
            {
                string headerLine = await stream.ReadLineAsync();
                while (!stream.EndOfStream)
                {
                    var line = await stream.ReadLineAsync();

                    if (line.Length >= 1)
                    {
                        var category = new CoursCategorie
                        {
                            NomCoursCategorie = line
                        };

                        categories.Add(category);
                    }
                }
            }

            _context.CoursCategories.AddRange(categories);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
