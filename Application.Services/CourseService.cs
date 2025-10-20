using Data;
using Domain.Model;
using DTOs;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class CourseService
    {
        private void ValidarCourseDTO(CourseDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Los datos del curso no pueden ser nulos.");

            if (dto.Cupo <= 0 || dto.Cupo > 1000)
                throw new ArgumentException("El cupo debe estar entre 1 y 1000.");

            int añoActual = DateTime.Now.Year;
            if (dto.Año_calendario < 2000 || dto.Año_calendario > añoActual + 1)
                throw new ArgumentException("Año calendario inválido.");

            if (string.IsNullOrWhiteSpace(dto.Turno))
                throw new ArgumentException("El turno es obligatorio.");

            if (dto.Turno.Length > 50)
                throw new ArgumentException("El turno no puede superar los 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Comision))
                throw new ArgumentException("La comisión es obligatoria.");

            if (dto.Comision.Length > 10)
                throw new ArgumentException("La comisión no puede superar los 10 caracteres.");

            if (!Regex.IsMatch(dto.Comision, @"^[a-zA-Z0-9]+$"))
                throw new ArgumentException("La comisión solo puede contener letras y números (sin espacios ni símbolos).");
        }

        public CourseDTO Add(CourseDTO dto)
        {
            ValidarCourseDTO(dto);

            var courseRepository = new CourseRepository();

            if (courseRepository.Exists(dto.Año_calendario, dto.Comision))
            {
                throw new ArgumentException($"Ya existe un curso en el año {dto.Año_calendario} con la comisión '{dto.Comision}'.");
            }

            Course course = new Course(
                0,
                dto.Cupo,
                dto.Año_calendario,
                dto.Turno,
                dto.Comision
            );

            courseRepository.Add(course);

            dto.Id = course.Id;
            return dto;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser mayor que cero.");

            var courseRepository = new CourseRepository();
            return courseRepository.Delete(id);
        }

        public CourseDTO? Get(int id)
        {
            if (id <= 0)
                return null;

            var courseRepository = new CourseRepository();
            Course? course = courseRepository.Get(id);

            if (course == null)
                return null;

            return new CourseDTO
            {
                Id = course.Id,
                Cupo = course.Cupo,
                Año_calendario = course.Año_calendario,
                Turno = course.Turno,
                Comision = course.Comision
            };
        }

        public IEnumerable<CourseDTO> GetAll()
        {
            var courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();

            return courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                Cupo = course.Cupo,
                Año_calendario = course.Año_calendario,
                Turno = course.Turno,
                Comision = course.Comision
            }).ToList();
        }

        public bool Update(CourseDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Los datos del curso no pueden ser nulos.");

            ValidarCourseDTO(dto);

            var courseRepository = new CourseRepository();

            if (courseRepository.Exists(dto.Año_calendario, dto.Comision, dto.Id))
            {
                throw new ArgumentException($"Ya existe otro curso en el año {dto.Año_calendario} con la comisión '{dto.Comision}'.");
            }

            Course course = new Course(
                dto.Id,
                dto.Cupo,
                dto.Año_calendario,
                dto.Turno,
                dto.Comision
            );

            return courseRepository.Update(course);
        }
    }
}
