namespace Domain.Model
{
    public class Course
    {
	    public int Id { get; private set; }
        public int Cupo { get; private set; }
        public int Año_calendario { get; private set; }
        public string Turno { get; private set; }
        public string Comision { get; private set; }
        public bool IsDeleted { get;  set; }
        public ICollection<CourseSubject> CoursesSubjects { get; set; } = new List<CourseSubject>();
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







        public Course (int id, int cupo, int año_calendario, string turno, string comision, int specialtyId, bool isDeleted = false)
        {
            SetId(id);
            SetCupo(cupo);
            SetAño_calendario(año_calendario);
            SetTurno(turno);
            SetComision(comision);
            SetSpecialtyId(specialtyId);
            IsDeleted = isDeleted;
        }

        public void SetId (int id)
        {
            if (id < 0)
                     throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
                Id = id;
        }

        public void SetCupo(int cupo)
        {
            if (cupo < 0)
                throw new ArgumentException("El cupo debe ser mayor que 0.", nameof(cupo));
            Cupo = cupo;
        }

        public void SetAño_calendario(int año_calendario)
        {
            if (año_calendario < 0)
                throw new ArgumentException("El año calendario debe ser mayor que 0.", nameof(año_calendario));
            Año_calendario = año_calendario;
        }

        public void SetTurno(string turno)
        {
            if (string.IsNullOrWhiteSpace(turno))
                throw new ArgumentException("Los turnos de los cursos no puede ser nulo o vacío.", nameof(turno));
            Turno = turno;
        }

        public void SetComision(string comision)
        {
            if (string.IsNullOrWhiteSpace(comision))
                throw new ArgumentException("Las comisiones de los cursos no puede ser nulo o vacío.", nameof(comision));
            Comision = comision;
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