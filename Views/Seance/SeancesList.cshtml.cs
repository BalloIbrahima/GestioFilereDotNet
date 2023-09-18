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
    public class SeancesListModel : PageModel
    {
      private readonly ApplicationDbContext _db;
      public SeancesListModel(ApplicationDbContext db ){
            _db = db;
       }
       public IList<Seance> seances {get ; private set ;}
       public IList<Sujet> subjects {get ; private set ;}
       public IList<Filiere> filieres {get ; private set ;}
       public IList<User> utilisateurs {get ; private set ;}
       public IList<Salle> salles {get ; private set ;}


        public void OnGet()
        {
            seances = _db.Seances.ToList();
            subjects = _db.Subjects.ToList();
            filieres = _db.Filieres.ToList();
            utilisateurs = _db.Utilisateurs.ToList();
            salles = _db.Salles.ToList();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
             var se =await _db.Seances.FindAsync(id);
             
             if(se == null){
                 return NotFound();   
             }
            _db.Seances.Remove(se);
            await _db.SaveChangesAsync(); 
            return RedirectToPage();
             
        }
    }
}
