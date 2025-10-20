using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class CourseSubject
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        //Atributos de la relacion
        public string DiaHoraDictado { get; set; }

        // Relación con usuarios (docentes y estudiantes)
        public ICollection<UserCourseSubject> Users { get; set; } = new List<UserCourseSubject>();

    }
}
