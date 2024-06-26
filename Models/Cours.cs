﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LearnHubBackOffice.Models
{
    public class Cours
    {
        [Key]
        public int IdCours { get; set; }
        [Required]
        public string TitreCours { get; set; }

        [ForeignKey("Formateur")]
        public int IdFormateur { get; set; }

        [ForeignKey("CoursCategorie")]
        public int IdCoursCategorie { get; set; }

     
        public DateTime DateCreationCours { get; set; }
        
        public DateTime DateModificationCours { get; set; }

       
        public Formateur Formateur { get; set; }
       
        public CoursCategorie CoursCategorie { get; set; }

        public string? Description { get; set; }
    }
}
