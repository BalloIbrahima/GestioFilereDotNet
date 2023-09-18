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
    public class EditFiliereModel : PageModel
    {
        private readonly ApplicationDbContext _db;
		[BindProperty]
		public Filiere filiere {get;set;}
         public EditFiliereModel(ApplicationDbContext db ){
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
             filiere = await _db.Filieres.FindAsync(id);
             if(filiere == null){
                 return NotFound();
             }else return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            
            if(!ModelState.IsValid){                
                return Page();
            }
            
            var DB_Fil = await _db.Filieres.FindAsync(filiere.IdFiliere);
            DB_Fil.NomFiliere = filiere.NomFiliere;
            DB_Fil.StudentsNombre = filiere.StudentsNombre;
             try
             {
                 await _db.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 throw;
             }
             return RedirectToPage("/Filiere/FiliereList");
        }
    }
}
