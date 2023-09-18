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
    //  [Authorize(Roles = "Admin")]
    public class EditStudentModel : PageModel
    {
          private readonly ApplicationDbContext _db;
		[BindProperty]
		public Etudiant student {get;set;}
        public IList<Filiere> filieres {get ; private set ;}

         public EditStudentModel(ApplicationDbContext db ){
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
             student =await _db.Students.FindAsync(id);
             filieres = _db.Filieres.ToList();

             if(student == null){
                 return NotFound();
             }else return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            
            if(!ModelState.IsValid){                
                return Page();
            }
            
            var DB_Etud = await _db.Students.FindAsync(student.IdEtudiant);
            DB_Etud.FullName = student.FullName;
            DB_Etud.IdFiliere = student.IdFiliere;
            
             try
             {
                 await _db.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 throw;
             }
             return RedirectToPage("/Student/StudentsList");
        }
    }
}
