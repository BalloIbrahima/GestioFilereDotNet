using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniProjet_Core.Data;
using MiniProjet_Core.Model;

namespace MyApp.Namespace
{        
   [Authorize(Roles = "Professeur")]
    public class ProfseanceModel : PageModel
    {
      private readonly ApplicationDbContext _db;
      private readonly UserManager<IdentityUser> _userManager;
      public ProfseanceModel(ApplicationDbContext db ,UserManager<IdentityUser> userManager){
            _db = db;
            _userManager = userManager;
       }
       public IList<Seance> seances {get ; private set ;}
       public IList<Seance> seancesP {get ; private set ;}

       public IList<Sujet> subjects {get ; private set ;}
       public IList<Filiere> filieres {get ; private set ;}
       public IList<Salle> salles {get ; private set ;}
       public IList<User> utilisateurs {get ; private set ;}


        public void OnGet()
        {
            string user = _userManager.GetUserId(HttpContext.User);
            subjects = _db.Subjects.ToList();
            filieres = _db.Filieres.ToList();
            salles = _db.Salles.ToList();
            seances = _db.Seances.ToList();
            var prof = _db.Utilisateurs.FirstOrDefault(x=> x.Id == user);

            seancesP =( from sn in seances
                    join sub in subjects 
                    on sn.IdSubject equals sub.IdSubjet
                    where  sub.IdProf == prof.Id
                    select sn).ToList();

        }
    }
}
