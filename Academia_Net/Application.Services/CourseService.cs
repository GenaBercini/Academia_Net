using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class CourseService
    {
        public CourseDTO Add(CourseDTO dto)
        {
            var courseRepository = new CourseRepository();

            // Validación: que no exista otro curso con misma comisión y año
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
            var courseRepository = new CourseRepository();
            return courseRepository.Delete(id);
        }

        public CourseDTO? Get(int id)
        {
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
            var courseRepository = new CourseRepository();

            // Validación de duplicado excluyendo el curso actual
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
