using System;

namespace Domain.Model
{
    public class Specialty
    {
        public int Id { get; private set; }
        public string DescEspecialidad { get; private set; }
        public int DuracionAnios { get; private set; }
        public bool Habilitado { get; set; }
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();


        public Specialty(int id, string descEspecialidad, int duracionAnios, bool habilitado = true)
        {
            SetId(id);
            SetDescEspecialidad(descEspecialidad);
            SetDuracionAnios(duracionAnios);
            Habilitado = habilitado;
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetDescEspecialidad(string descEspecialidad)
        {
            if (string.IsNullOrWhiteSpace(descEspecialidad))
                throw new ArgumentException("La descripción de la especialidad no puede ser nula o vacía.", nameof(descEspecialidad));
            DescEspecialidad = descEspecialidad;
        }

        public void SetDuracionAnios(int duracionAnios)
        {
            if (duracionAnios < 0)
                throw new ArgumentException("La duración en años debe ser mayor que 0.", nameof(duracionAnios));
            DuracionAnios = duracionAnios;
        }
    }
}

