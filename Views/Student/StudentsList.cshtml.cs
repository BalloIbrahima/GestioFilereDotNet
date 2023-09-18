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
    public class StudentsListModel : PageModel
    {      
        private readonly ApplicationDbContext _db;
              public IList<Filiere> filieres {get ; private set ;}

      public StudentsListModel(ApplicationDbContext db ){
            _db = db;
       }
       public IList<Etudiant> Students {get ; private set ;}
        public void OnGet()
        {
            Students = _db.Students.ToList();
            filieres = _db.Filieres.ToList();

        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
             var Student =await _db.Students.FindAsync(id);
             
             if(Student == null){
                 return NotFound();   
             }
            var fl = filieres.FirstOrDefault(s => s.IdFiliere == Student.IdFiliere);
            fl.StudentsNombre = fl.StudentsNombre-1;
            _db.Students.Remove(Student);
            await _db.SaveChangesAsync(); 
            return RedirectToPage();
             
        }
    
    }
}
