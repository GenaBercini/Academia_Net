using System;

namespace Domain.Model
{
    public class Subject
    {
        public int Id { get; private set; }
        public string Desc { get; private set; }
        public int HsSemanales { get; private set; }
        public bool Obligatoria { get; private set; }
        public int Año { get; private set; }
        public bool IsDeleted { get; set; }  

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<CourseSubject> CoursesSubjects { get; set; } = new List<CourseSubject>();

        private int _planId;
        private Plan? _plan;

        public int PlanId
        {
            get => _plan?.Id ?? _planId;
            private set => _planId = value;
        }

        public Plan? Plan
        {
            get => _plan;
            private set
            {
                _plan = value;
                if (value != null && _planId != value.Id)
                {
                    _planId = value.Id;
                }
            }
        }

        public Subject(int id, string desc, int hsSemanales, bool obligatoria, int año,int planId, bool isDeleted = false)
        {
            SetId(id);
            SetDesc(desc);
            SetHsSemanales(hsSemanales);
            SetObligatoria(obligatoria);
            SetAño(año);
            SetPlan(planId);
            IsDeleted = isDeleted;  
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
        public void SetAño(int año)
        {
            if (año <= 0 && año>5)
                throw new ArgumentException("El año debe ser un numero entre 0 y 5.", nameof(año));
            Año = año;
        }
        public void SetPlan(int planId)
        {
            if (planId <= 0)
                throw new ArgumentException("El PlanId debe ser mayor que 0.", nameof(planId));

            _planId = planId;

            if (_plan != null && _plan.Id != planId)
            {
                _plan = null;
            }
        }
  
    }
}
