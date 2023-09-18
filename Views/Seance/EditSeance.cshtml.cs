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
    public class EditSeanceModel : PageModel
    {
        private readonly ApplicationDbContext _db;
		[BindProperty]
		public Seance seance {get;set;}
        public IList<Sujet> subjects {get ; private set ;}
        public IList<Salle> salles {get ; private set ;}
         public EditSeanceModel(ApplicationDbContext db ){
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
             salles = _db.Salles.ToList();
            subjects = _db.Subjects.ToList();
            
             seance = await _db.Seances.FindAsync(id);
             if(seance == null){
                 return NotFound();
             }else return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            
            if(!ModelState.IsValid){                
                return Page();
            }
            
            var DB_Sea = await _db.Seances.FindAsync(seance.IdSeance);
            DB_Sea.IdSubject = seance.IdSubject;
            DB_Sea.NbreParjour = seance.NbreParjour;
            DB_Sea.DateSeance = seance.DateSeance;
            DB_Sea.NumSalle = seance.NumSalle;
            DB_Sea.SeanceTitre = seance.SeanceTitre;
            
             try
             {
                 await _db.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 throw;
             }
             return RedirectToPage("/Seance/SeancesList");
        }
    }
}
