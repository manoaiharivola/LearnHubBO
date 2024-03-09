using System.ComponentModel.DataAnnotations;

namespace LearnHubBackOffice.Models
{
    public class Formateur
    {
        [Key]
        public int IdFormateur { get; set; }
        [Required]
        public string NomFormateur { get; set; }
    }
}
