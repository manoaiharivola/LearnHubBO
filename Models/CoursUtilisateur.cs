using LearnHubBackOffice.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnHubBO.Models
{
    public class CoursUtilisateur
    {
        [Key]
        public int IdCoursUtilisateur { get; set; }

        [ForeignKey("Cours")]
        public int IdCours { get; set; }

        public Cours Cours { get; set; }

        [ForeignKey("Utilisateur")]
        public int IdUtilisateur { get; set; }

        public Utilisateur Utilisateur { get; set; }

        public DateTime DateCreationCoursUtilisateur { get; set; }
    }
}
