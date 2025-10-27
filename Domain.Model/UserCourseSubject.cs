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
        public Course Course { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public CourseSubject CourseSubject { get; set; }

        public decimal? NotaFinal { get; set; }
        public DateTime? FechaInscripcion { get; set; }
    
        public DateTime EnrollmentDate { get; set; }
    }
}
