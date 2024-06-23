using LearnHubBackOffice.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnHubBO.Models
{
    public class ChapitreUtilisateur
    {
        [Key]
        public int IdChapitreUtilisateur { get; set; }

        [ForeignKey("Chapitre")]
        public int IdChapitre { get; set; }

        public Chapitre Chapitre { get; set; }

        [ForeignKey("Utilisateur")]
        public int IdUtilisateur { get; set; }

        public Utilisateur Utilisateur { get; set; }

        public DateTime DateCreationChapitreUtilisateur { get; set; }
    }
}
