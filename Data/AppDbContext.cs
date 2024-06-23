using LearnHubBackOffice.Models;
using LearnHubBO.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnHubBackOffice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<CoursCategorie> CoursCategories { get; set; }
        public DbSet<Cours> Courses { get; set; }
        public DbSet<Chapitre> Chapitres { get; set; }

        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DbSet<CoursUtilisateur> CoursUtilisateur { get; set; }
        public DbSet<ChapitreUtilisateur> ChapitreUtilisateur { get; set; }
    }
}
