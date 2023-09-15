using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjet_Core.Model
{
    public class Salle
    {
        [Key]
        public int NumSalle { get; set; }

    }
}