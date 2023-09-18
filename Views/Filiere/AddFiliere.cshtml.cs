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
    public class AddFiliereModel : PageModel
    {
      private readonly ApplicationDbContext _db;
      [BindProperty]
      public Filiere filiere {get;set;}
      public AddFiliereModel(ApplicationDbContext db ){
            _db = db;
        }
        public IActionResult OnPost(){

            if(ModelState.IsValid){
                var DB_Fil = _db.Filieres.Find(filiere.IdFiliere);
                if(DB_Fil != null){
                    return Page();    
                }
                _db.Filieres.Add(filiere);
                _db.SaveChanges();
                return RedirectToPage("/Filiere/FiliereList");
            }else{
                return Page();
            }
        }
    }
}
