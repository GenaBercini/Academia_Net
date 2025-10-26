using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{

    public class UserCourseSubjectDTO
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public decimal? NotaFinal { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}

