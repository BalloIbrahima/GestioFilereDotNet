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
    public class termListModel : PageModel
    {
         private readonly ApplicationDbContext _db;
      public termListModel(ApplicationDbContext db ){
            _db = db;
       }
       public IList<Etudiant> students {get ; private set ;}
       public IList<Salle> salles {get ; private set ;}
       public IList<Seance> seances {get ; private set ;}
       public IList<Presence> presences {get ; private set ;}


        public void OnGet()
        {
            students = _db.Students.ToList();
            salles = _db.Salles.ToList();
            seances = _db.Seances.ToList();
            presences = _db.Presences.ToList();

        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
             var Sub =await _db.Presences.FindAsync(id);
             
             if(Sub == null){
                 return NotFound();   
             }
            _db.Presences.Remove(Sub);
            await _db.SaveChangesAsync(); 
            return RedirectToPage();
             
        }
    
    }
}
