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
    public class EditSubjectModel : PageModel
    {
        private readonly ApplicationDbContext _db;
		[BindProperty]
		public Sujet subject {get;set;}
        public IList<Filiere> filieres {get ; private set ;}
        public IList<User> users {get ; private set ;}
         public EditSubjectModel(ApplicationDbContext db ){
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(int id){
        
        
            filieres = _db.Filieres.ToList();
            users = _db.Utilisateurs.ToList();
        
             subject =await _db.Subjects.FindAsync(id);

             if(subject == null){
                 return NotFound();
             }else return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            
            if(!ModelState.IsValid){                
                return Page();
            }
            
            var DB_Sub = await _db.Subjects.FindAsync(subject.IdSubjet);
            DB_Sub.NomSubjet = subject.NomSubjet;
            DB_Sub.IdProf = subject.IdProf;
            DB_Sub.IdFiliere = subject.IdFiliere;
            
             try
             {
                 await _db.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 throw;
             }
             return RedirectToPage("/Subject/SubjectsList");
        }
    }
}
