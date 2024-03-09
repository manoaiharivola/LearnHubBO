using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBackOffice.Data;
using LearnHubBackOffice.Models;

namespace LearnHubBO.Pages.CoursCategories
{
    public class DetailsModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public DetailsModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        public CoursCategorie CoursCategorie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courscategorie = await _context.CoursCategories.FirstOrDefaultAsync(m => m.IdCoursCategorie == id);
            if (courscategorie == null)
            {
                return NotFound();
            }
            else
            {
                CoursCategorie = courscategorie;
            }
            return Page();
        }
    }
}
