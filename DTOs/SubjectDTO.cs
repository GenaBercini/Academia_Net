namespace DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Desc { get; set; } = string.Empty;
        public int HsSemanales { get; set; }
        public bool Obligatoria { get; set; }
        public int Año { get;  set; }
        public int PlanId { get; set; }
        public string? PlanDescripcion { get; set; }
    }
}
