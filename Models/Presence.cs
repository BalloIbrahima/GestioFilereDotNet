using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjet_Core.Model
{

    public class Presence
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPresence { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Time { get; set; }

        [Required]
        public int IdSeance { get; set; }

        [Required]
        public int NumSalle { get; set; }

        [Required]
        public int IdStudent { get; set; }

        
        [ForeignKey("IdSeance")]
        public Seance seance { get; set; }

        [ForeignKey("NumSalle")]
        public Salle salle { get; set; }

        [ForeignKey("IdStudent")]
        public Etudiant student { get; set; }
    }
}