namespace Domain.Model
{
    public class Course
    {
	    public int Id { get; private set; }
        public int Cupo { get; private set; }
        public int Año_calendario { get; private set; }
        public string Turno { get; private set; }
        public int Comision { get; private set; }
        public bool IsDeleted { get;  set; }

 

        public Course (int id, int cupo, int año_calendario, string turno, int comision)
        {
            SetId(id);
            SetCupo(cupo);
            SetAño_calendario(año_calendario);
            SetTurno(turno);
            SetComision(comision);  
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

        public void SetComision(int comision)
        {
            if (comision < 0)
                throw new ArgumentException("La comisión debe ser mayor que 0.", nameof(comision));
            Comision = comision;
        }
      
    }
}