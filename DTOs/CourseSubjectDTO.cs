using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CourseSubjectDTO
    {
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public string? DiaHoraDictado { get; set; }
        public SubjectDTO? Subject { get; set; }
    }
}
