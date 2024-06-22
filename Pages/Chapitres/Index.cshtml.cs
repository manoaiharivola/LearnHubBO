using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnHubBO.Models;
using LearnHubBackOffice.Data;

namespace LearnHubBO.Pages.Chapitres
{
    public class IndexModel : PageModel
    {
        private readonly LearnHubBackOffice.Data.AppDbContext _context;

        public IndexModel(LearnHubBackOffice.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Chapitre> Chapitre { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Chapitre = await _context.Chapitres
                .Include(c => c.Cours).ToListAsync();
        }
    }
}
