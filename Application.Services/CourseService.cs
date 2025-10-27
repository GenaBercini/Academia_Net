using Data;
using Domain.Model;
using DTOs;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class CourseService
    {
        private readonly CourseRepository _courseRepository;
        private readonly SpecialtyRepository _specialtyRepository;

        public CourseService(CourseRepository courseRepository, SpecialtyRepository specialtyRepository)
        {
            _courseRepository = courseRepository;
            _specialtyRepository = specialtyRepository;
        }
        private void ValidarCourseDTO(CourseDTO dto, bool isUpdate = false)
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


        public async Task<CourseDTO> AddAsync(CourseDTO dto)
        {
            ValidarCourseDTO(dto);
            //var courseRepository = new CourseRepository();
            Course course = new Course(
                0,
                dto.Cupo,
                dto.Año_calendario,
                dto.Turno,
                dto.Comision,
                dto.SpecialtyId
            );
            await _courseRepository.AddAsync(course);
            dto.Id = course.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var courseRepository = new CourseRepository();
            var course = await _courseRepository.GetAsync(id);
            if (course == null)
                return false;
            return await _courseRepository.DeleteAsync(id);
        }
     
        public async Task<CourseDTO?> GetAsync(int id)
        {
       
            /*var courseRepository = new CourseRepository()*/;
            Course? course = await _courseRepository.GetAsync(id);

            if (course == null)
                return null;

            return new CourseDTO
            {
                Id = course.Id,
                Cupo = course.Cupo,
                Año_calendario = course.Año_calendario,
                Turno = course.Turno,
                Comision = course.Comision,
                SpecialtyId = course.SpecialtyId,
            };
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            var specialties = await _specialtyRepository.GetAllAsync();
            var courses = await _courseRepository.GetAllAsync();
            return courses
                .Where(c => !c.IsDeleted)
                .Select(course => new CourseDTO
            {
                Id = course.Id,
                Cupo = course.Cupo,
                Año_calendario = course.Año_calendario,
                Turno = course.Turno,
                Comision = course.Comision,
                SpecialtyId = course.SpecialtyId,
                SpecialtyDescripcion = specialties.FirstOrDefault(p => p.Id == course.SpecialtyId)?.DescEspecialidad
                }).ToList();
        }
      
        public async Task<bool> UpdateAsync(CourseDTO dto)
        {
            //var courseRepository = new CourseRepository();
            var existing = await _courseRepository.GetAsync(dto.Id);
            if (existing == null || existing.IsDeleted)
                throw new ArgumentException("El curso no existe o está deshabilitado.");
            ValidarCourseDTO(dto, isUpdate:true);
            var duplicate = (await _courseRepository.GetAllAsync())
                .FirstOrDefault(c =>
                        c.Id != dto.Id &&
                        c.Comision == dto.Comision &&
                        c.Turno == dto.Turno &&
                        c.Año_calendario == dto.Año_calendario &&
                        !c.IsDeleted);
            if (duplicate != null)
                throw new ArgumentException("Ya existe un curso con esa descripción y año.");
            Course course = new Course(
                dto.Id,
                dto.Cupo,
                dto.Año_calendario,
                dto.Turno,
                dto.Comision,
                dto.SpecialtyId
            );
            course.IsDeleted = existing.IsDeleted;

            return await _courseRepository.UpdateAsync(course);
        }

        public async Task<IEnumerable<CourseSubjectDTO>> GetSubjectsByCourse(int courseId)
        {
            //var repo = new CourseRepository();
            var items = await _courseRepository.GetCourseSubjects(courseId);
            return items.Select(cs => new CourseSubjectDTO
            {
                CourseId = cs.CourseId,
                SubjectId = cs.SubjectId,
                DiaHoraDictado = cs.DiaHoraDictado,
                Subject = cs.Subject == null ? null : new SubjectDTO
                {
                    Id = cs.Subject.Id,
                    Desc = cs.Subject.Desc,
                    HsSemanales = cs.Subject.HsSemanales,
                    Obligatoria = cs.Subject.Obligatoria,
                    Año= cs.Subject.Año,
                }
            }).ToList();
        }

        public async Task<CourseSubjectDTO> AddSubjectToCourse(int courseId, int subjectId, string? diaHora)
        {
            //var repo = new CourseRepository();
            var cs = await _courseRepository.AddCourseSubject(courseId, subjectId, diaHora);
            return new CourseSubjectDTO
            {
                CourseId = cs.CourseId,
                SubjectId = cs.SubjectId,
                DiaHoraDictado = cs.DiaHoraDictado,
                Subject = cs.Subject == null ? null : new SubjectDTO
                {
                    Id = cs.Subject.Id,
                    Desc = cs.Subject.Desc,
                    HsSemanales = cs.Subject.HsSemanales,
                    Obligatoria = cs.Subject.Obligatoria,
                    Año = cs.Subject.Año,
                }
            };
        }
    }
}
