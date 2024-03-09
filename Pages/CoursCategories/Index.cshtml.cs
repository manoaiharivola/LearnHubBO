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
    public class IndexModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public IndexModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<CoursCategorie> CoursCategorie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            CoursCategorie = await _context.CoursCategories.ToListAsync();
        }
    }
}
