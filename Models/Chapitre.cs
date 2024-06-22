using LearnHubBackOffice.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnHubBO.Models
{
    public class Chapitre
    {
        [Key]
        public int IdChapitre { get; set; }

        [Required]
        public string TitreChapitre { get; set; }

        [Required]
        public int Ordre { get; set; }

        [Required]
        public string Contenu { get; set; }

        [ForeignKey("Cours")]
        public int IdCours { get; set; }

        public Cours Cours { get; set; }

        public DateTime DateCreationChapitre { get; set; }

        public DateTime DateModificationChapitre { get; set; }
    }
}
