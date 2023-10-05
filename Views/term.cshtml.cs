using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniProjet_Core.Data;
using MiniProjet_Core.Model;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Terminal")]

    public class termModel : PageModel
    {
      private readonly ApplicationDbContext _db;

      [BindProperty]
      public Presence presence {get;set;}
      public IList<Salle> salles {get ; private set ;}

      public IList<Seance> seances { get; private set; }
      public IList<Etudiant> students {get ; private set ;}
      public IList<Filiere> filieres {get ; private set ;}
      public IList<Sujet> subjects {get ; private set ;}
      public IList<Presence> presences {get ; private set ;}



        public termModel(ApplicationDbContext db ){
            _db = db;
        }
        public void OnGet()
        {
            salles = _db.Salles.ToList();   
            students = _db.Students.ToList();
            filieres = _db.Filieres.ToList();   
            //seances = _db.Seances.ToList();
            subjects = _db.Subjects.ToList();
            var now = DateTime.Now.ToString("yyyy-MM-dd");
            var time = DateTime.Now.ToString("HH");

            seances = _db.Seances.Where(s => s.DateSeance == now && s.NbreParjour == time).ToList();
        }
        public IActionResult OnPost(){

            salles = _db.Salles.ToList();
            students = _db.Students.ToList();
            filieres = _db.Filieres.ToList();
            subjects = _db.Subjects.ToList();
            seances = _db.Seances.ToList();
           

            if (ModelState.IsValid){
//Salle
                var Location = from s2 in seances
                where s2.IdSeance == presence.IdSeance
                select s2 ;
//--
                var studentAndSeanace = from std in students
				  join fl in filieres
				  on std.IdFiliere equals fl.IdFiliere
				  join sub in subjects
				  on fl.IdFiliere equals sub.IdFiliere  
				  join s1 in seances
				  on  sub.IdSubjet equals s1.IdSubject 
				  where std.IdEtudiant == presence.IdStudent && s1.IdSeance == presence.IdSeance
				  select new { abijou = std.IdEtudiant , med = s1.IdSeance};

            //si l'etudiant deja marqée la presence
 
            //    if(_db.Presences.Where(s => s.IdStudent == presence.IdStudent && s.IdSeance == presence.IdSeance) == null){return RedirectToPage("/redirect");}
            //si l'etudiant choisissez la bon salle ou pas 
                if(Location.ElementAt(0).NumSalle != presence.NumSalle) { return RedirectToPage("/term"); }
            //si l'etudiant a le droit d'acces a cette seance ou non (inscrit dans le fillier)
                if(!studentAndSeanace.Any()) { return RedirectToPage("/term"); }
                if(presence.IdStudent == studentAndSeanace.ElementAt(0).abijou &&
                 presence.IdSeance == studentAndSeanace.ElementAt(0).med ){

                    var tmp = _db.Seances.Find(presence.IdSeance);
                    tmp.nbrPre = tmp.nbrPre+1;
                try
                {
                     _db.Presences.Add(presence);
                     _db.SaveChanges();
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
                  
                    return RedirectToPage("/termList");
                     }else{
                         return RedirectToPage("/term");
                    }
            }else{
                return RedirectToPage("/term");
            }
             
        }
    }
}
