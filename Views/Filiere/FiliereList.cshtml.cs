using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniProjet_Core.Data;
using MiniProjet_Core.Model;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Admin")]
    public class FiliereListModel : PageModel
    { private readonly ApplicationDbContext _db;
      public FiliereListModel(ApplicationDbContext db ){
            _db = db;
       }
       public IList<Filiere> filieres {get ; private set ;}
        public void OnGet()
        {
            filieres = _db.Filieres.ToList();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
             var Filiere =await _db.Filieres.FindAsync(id);
            
             if(Filiere == null){
                 return NotFound();   
             }
             
            _db.Filieres.Remove(Filiere);
            await _db.SaveChangesAsync(); 
            return RedirectToPage();
             
        }
    }
}
