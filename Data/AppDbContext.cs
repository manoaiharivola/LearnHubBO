using LearnHubBackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnHubBackOffice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<CoursCategorie> CoursCategories { get; set; }
        public DbSet<Cours> Courses { get; set; }
    }
}
