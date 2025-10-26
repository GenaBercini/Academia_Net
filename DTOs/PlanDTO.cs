

namespace DTOs
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public int Año_calendario { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int SpecialtyId { get; set; }
        public string? SpecialtyDescripcion { get; set; }


    }
}
