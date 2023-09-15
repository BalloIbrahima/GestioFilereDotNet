using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace MiniProjet_Core.Model
{
    public class Seance
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int IdSeance { get; set; }

        [Required]
        public string SeanceTitre { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]

        public string DateSeance { get; set; }
        [Required]
        public string NbreParjour { get; set; }//il y a 4 Seance par jour

        public int nbrPre { get; set; }
        
        [Required]
        public int IdSubject { get; set; }
        [Required]
        public int NumSalle { get; set; }


        [ForeignKey("IdSubject")]
        public Sujet Subject { get; set; }

        [ForeignKey("NumSalle")]
        public Salle Salle { get; set; }

    }
}