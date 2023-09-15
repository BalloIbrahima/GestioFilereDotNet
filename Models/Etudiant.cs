using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjet_Core.Model
{
    public class Etudiant
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdEtudiant { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int IdFiliere { get; set; }

        [ForeignKey("IdFiliere")]
        public Filiere Filiere { get; set; }

    }
}