using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class UserCourseSubject
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int CourseId { get; set; }
        public int SubjectId { get; set; }

        // Relación con el par Curso-Materia
        public CourseSubject CourseSubject { get; set; }

        // Atributos que existen solo si el usuario es estudiante
        public decimal? NotaFinal { get; set; }
        public DateTime? FechaInscripcion { get; set; }
    }
}
