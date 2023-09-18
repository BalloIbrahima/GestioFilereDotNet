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
    //  [Authorize(Roles = "Admin")]
    public class SubjectsListModel : PageModel
    {
           private readonly ApplicationDbContext _db;
      public SubjectsListModel(ApplicationDbContext db ){
            _db = db;
       }
       public IList<Sujet> subjects {get ; private set ;}
       public IList<Filiere> filieres {get ; private set ;}
       public IList<User> utilisateurs {get ; private set ;}
       

        public void OnGet()
        {
            subjects = _db.Subjects.ToList();
            filieres = _db.Filieres.ToList();
            utilisateurs = _db.Utilisateurs.ToList();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
             var Sub =await _db.Subjects.FindAsync(id);
             
             if(Sub == null){
                 return NotFound();   
             }
            _db.Subjects.Remove(Sub);
            await _db.SaveChangesAsync(); 
            return RedirectToPage();
             
        }
    
    }
}
