using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MiniProjet_Core.Model
{
    public class Sujet
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdSubjet { get; set; }

        [Required]
        public string NomSubjet { get; set; }

        [Required]
        public string IdProf { get; set; }

        [Required]
        public int IdFiliere { get; set; }

        

        [ForeignKey("IdFiliere")]
        public Filiere Filiere { get; set; }

        [ForeignKey("IdProf")]
        public User utilisateur { get; set; }
    }

}