using System.Text.RegularExpressions;

namespace DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public int Cupo { get; set; }
        public int Año_calendario { get; set; }
        public string Turno { get; set; } = string.Empty;
        public string Comision { get; set; } = string.Empty;
        public int SpecialtyId { get; set; }
        public string? SpecialtyDescripcion { get; set; }

        public IEnumerable<CourseSubjectDTO>? CourseSubjects { get; set; }
    }
}
