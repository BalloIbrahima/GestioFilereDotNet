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
     [Authorize(Roles = "Admin")]
      public class AddSeanceModel : PageModel
    {
       
       private readonly ApplicationDbContext _db;
      [BindProperty]
  
      public Seance seance {get;set;}
      public IList<Sujet> subjects {get ; private set ;}
      public IList<Salle> salles {get ; private set ;}
      public IList<User> users {get ; private set ;}
     // public IList<Seance> listeSeance {get ; private set ;}


      public AddSeanceModel(ApplicationDbContext db ){
            _db = db;
        }
        public void OnGet()
        {
            users = _db.Utilisateurs.ToList();
            salles = _db.Salles.ToList();
            subjects = _db.Subjects.ToList();
          //  listeSeance = _db.Seances.ToList();

        }
        public IActionResult OnPost(){

            if(ModelState.IsValid){
                var DB_Sub = _db.Seances.Find(seance.IdSeance);
                if(DB_Sub != null){
                    return Page();    
                }
             var temp = _db.Seances.Find(seance.NumSalle);
             if(temp != null){
                    if(temp.NbreParjour == seance.NbreParjour){
                    return RedirectToPage("/Seance/SeancesList");
                }
             }
                _db.Seances.Add(seance);
                _db.SaveChanges();
                return RedirectToPage("/Seance/SeancesList");
            }else{
                return Page();
            }
        }
    }
}
