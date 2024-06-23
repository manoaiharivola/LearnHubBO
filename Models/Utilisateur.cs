using Microsoft.CodeAnalysis.Scripting;
using System.ComponentModel.DataAnnotations;

namespace LearnHubBackOffice.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }

        [Required]
        public string NomUtilisateur { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MotDePasseHash { get; set; }

        public void SetPassword(string password)
        {
            MotDePasseHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, MotDePasseHash);
        }
    }
}
