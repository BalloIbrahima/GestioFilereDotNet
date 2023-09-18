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
    public class ProfListModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IList<Sujet> subjects {get ; private set ;}
        public IList<User> professors {get ; private set ;}


      public ProfListModel(ApplicationDbContext db ){
            _db = db;
       }
        public void OnGet()
        {
            professors = _db.Utilisateurs.Where(s=> s.Status == 'P').ToList();
            subjects = _db.Subjects.ToList();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
             var Student =await _db.Students.FindAsync(id);
             
             if(Student == null){
                 return NotFound();   
             }
            _db.Students.Remove(Student);
            await _db.SaveChangesAsync(); 
            return RedirectToPage();
        }
    
    }
}
