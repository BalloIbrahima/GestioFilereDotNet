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
    public class AddStudentModel : PageModel
    {
     private readonly ApplicationDbContext _db;
      [BindProperty]
      public Etudiant student {get;set;}
      public IList<Filiere> filieres {get ; private set ;}
      public AddStudentModel(ApplicationDbContext db ){
            _db = db;
        }
        public void OnGet()
        {
            filieres = _db.Filieres.ToList();
        }
        public IActionResult OnPost(){

            if(ModelState.IsValid){
                var fl = _db.Filieres.Find(student.IdFiliere);
                fl.StudentsNombre = fl.StudentsNombre+1;
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToPage("/Student/StudentsList");
            }else{
                return Page();
            }
        }
    }
}
