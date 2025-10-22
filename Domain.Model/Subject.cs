using System;

namespace Domain.Model
{
    public class Subject
    {
        public int Id { get; private set; }
        public string Desc { get; private set; }
        public int HsSemanales { get; private set; }
        public bool Obligatoria { get; private set; }
        public bool Habilitado { get; set; }

        public int PlanId { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<CourseSubject> CoursesSubjects { get; set; } = new List<CourseSubject>();



        public Subject(int id, string desc, int hsSemanales, bool obligatoria, bool habilitado = true)
        {
            SetId(id);
            SetDesc(desc);
            SetHsSemanales(hsSemanales);
            SetObligatoria(obligatoria);
            Habilitado = habilitado;
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetDesc(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc))
                throw new ArgumentException("La descripción no puede ser nula o vacía.", nameof(desc));
            Desc = desc;
        }

        public void SetHsSemanales(int hsSemanales)
        {
            if (hsSemanales <= 0)
                throw new ArgumentException("Las horas semanales deben ser mayores que 0.", nameof(hsSemanales));
            HsSemanales = hsSemanales;
        }

        public void SetObligatoria(bool obligatoria)
        {
            Obligatoria = obligatoria;
        }
    }
}

