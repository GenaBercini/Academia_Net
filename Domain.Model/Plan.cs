namespace Domain.Model
{
    public class Plan
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public int Año_calendario { get; private set; }
        public bool IsDeleted { get;  set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        private int _specialtyId;
        private Specialty? _specialty;

        public int SpecialtyId
        {
            get => _specialty?.Id ?? _specialtyId;
            private set => _specialtyId = value;
        }

        public Specialty? Specialty
        {
            get => _specialty;
            private set
            {
                _specialty = value;
                if (value != null && _specialtyId != value.Id)
                {
                    _specialtyId = value.Id;
                }
            }
        }

        public Plan(int id, string descripcion, int año_calendario,int specialtyId, bool isDeleted = false)
        {
            SetId(id);
            SetDescripcion(descripcion);
            SetAño_calendario(año_calendario);
            SetSpecialtyId(specialtyId);
            IsDeleted = isDeleted;
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripcion del plan no puede ser nulo o vacío.", nameof(descripcion));
            Descripcion = descripcion;
        }

        public void SetAño_calendario(int año_calendario)
        {
            if (año_calendario < 0)
                throw new ArgumentException("El año calendario debe ser mayor que 0.", nameof(año_calendario));
            Año_calendario = año_calendario;
        }
        public void SetSpecialtyId(int specialtyId)
        {
            if (specialtyId <= 0)
                throw new ArgumentException("El SpecialtyId debe ser mayor que 0.", nameof(specialtyId));

            _specialtyId = specialtyId;

            if (_specialty != null && _specialty.Id != specialtyId)
            {
                _specialty = null;
            }
        }
    }
}