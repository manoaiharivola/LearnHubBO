using System.ComponentModel.DataAnnotations;

namespace LearnHubBackOffice.Models
{
    public class CoursCategorie
    {
        [Key]
        public int IdCoursCategorie { get; set; }
        [Required]
        public string NomCoursCategorie { get; set; }

    }
}
