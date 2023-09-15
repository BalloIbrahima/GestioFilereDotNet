using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjet_Core.Model
{
    public class User : IdentityUser
    {
        [Required]
        public string Nom { get; set; }
        [Required]

        public string Prenom { get; set; }

        public string CIN   { get; set; } 

        [DataType(DataType.Date)]
        [Display (Name = "Date de naissance" )]
        public string Date { get; set; }

        [Required]
        public char Status { get; set; }
    }
}