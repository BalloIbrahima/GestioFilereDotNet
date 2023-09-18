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
    [Authorize(Roles = "Professeur , Admin")]
    public class Presence_ListModel : PageModel
    {     private readonly ApplicationDbContext _db;
      public Presence_ListModel(ApplicationDbContext db ){
            _db = db;
       }
       public Seance seance {get;set;}

        public IList<Salle> salles { get; private set; }

        public IList<Seance> seances { get; private set; }
        public IList<Etudiant> students { get; private set; }
        public IList<Filiere> filieres { get; private set; }
        public IList<Sujet> subjects { get; private set; }
        public IList<Presence> presences { get; private set; }
        public IList<Etudiant> absence {get ; private set ;}
       

        public void OnGet(int id)
        {
            presences = _db.Presences.Where(o => o.IdSeance == id).ToList();
            students = _db.Students.ToList();
            filieres = _db.Filieres.ToList();
            subjects = _db.Subjects.ToList();
            seances = _db.Seances.ToList();
            seance = _db.Seances.FirstOrDefault(s => s.IdSeance == id);
            students =  (from std in students
				  join fl in filieres
                  on std.IdFiliere equals fl.IdFiliere
				  join sub in subjects
                  on fl.IdFiliere equals sub.IdFiliere  
				  join s1 in seances
				  on  sub.IdSubjet equals s1.IdSubject 
				  where s1.IdSeance == id
				  select std).ToList();

            absence = students.Where(st => !presences.Select(pr => pr.IdStudent).Contains(st.IdEtudiant)).ToList();
                }
        
    
    }
}
