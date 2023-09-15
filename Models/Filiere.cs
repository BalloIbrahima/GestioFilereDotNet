using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjet_Core.Model
{
    public class Filiere
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFiliere { get; set; }

        [Required]
        public string NomFiliere { get; set; }

        public int StudentsNombre { get; set; }

    }
}