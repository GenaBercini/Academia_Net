using System.Text.RegularExpressions;

namespace DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public int Cupo { get; set; }
        public int Año_calendario { get; set; }
        public string Turno { get; set; }
        public int Comision { get; set; }
    }
}
