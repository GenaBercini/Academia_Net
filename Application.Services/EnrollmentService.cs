using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EnrollmentService
    {
        private readonly EnrollmentRepository _enrollmentRepository;
        private readonly UserRepository _userRepository;

        public EnrollmentService(UserRepository userRepository, EnrollmentRepository enrollmentRepository)
        {
            _userRepository = userRepository;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<UserCourseSubjectDTO?> GetEnrollmentAsync(int userId, int courseId, int subjectId)
        {
            var entity = await _enrollmentRepository.GetAsync(userId, courseId, subjectId);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<IEnumerable<UserCourseSubjectDTO>> GetEnrollmentsByUserAndCourseAsync(int userId, int courseId)
        {
            var enrollments = await _enrollmentRepository.GetByUserAsync(userId);
            var filtered = enrollments.Where(e => e.CourseId == courseId);
            return filtered.Select(MapToDto).ToList();
        }

        public async Task<bool> EnrollUserInCourseSubjectAsync(int userId, int courseId, int subjectId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null || user.Status != UserStatus.Active)
                throw new InvalidOperationException("Usuario no encontrado o inactivo.");

            if (user.TypeUser != UserType.Student && user.TypeUser != UserType.Teacher)
                throw new InvalidOperationException("Solo docentes o estudiantes pueden inscribirse en materias.");

            var existing = await _enrollmentRepository.GetAsync(userId, courseId, subjectId);
            if (existing != null)
                throw new InvalidOperationException("El usuario ya está inscripto en esta materia.");

            var enrollment = new UserCourseSubject
            {
                UserId = userId,
                CourseId = courseId,
                SubjectId = subjectId,
                FechaInscripcion = DateTime.Now
            };

            await _enrollmentRepository.AddAsync(enrollment);
            return true;
        }

        public async Task AddEnrollmentAsync(UserCourseSubjectDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await _userRepository.GetAsync(dto.UserId);
            if (user == null)
                throw new ArgumentException($"Usuario con id {dto.UserId} no existe.");
            if (user.Status != UserStatus.Active)
                throw new ArgumentException($"Usuario con id {dto.UserId} no está activo.");

            var course = await _enrollmentRepository.GetCourseAsync(dto.CourseId);
            var subject = await _enrollmentRepository.GetSubjectAsync(dto.SubjectId);

            if (course == null)
                throw new ArgumentException($"Curso con id {dto.CourseId} no existe.");
            if (subject == null)
                throw new ArgumentException($"Materia con id {dto.SubjectId} no existe.");

            int añoCurso = 0;
            if (!string.IsNullOrEmpty(course.Comision) && char.IsDigit(course.Comision[0]))
                añoCurso = int.Parse(course.Comision[0].ToString());

            bool courseSubjectExists = await _enrollmentRepository.CourseSubjectExistsAsync(dto.CourseId, dto.SubjectId);

            if (!courseSubjectExists && subject.Año == añoCurso)
            {
                var newCourseSubject = new CourseSubject
                {
                    CourseId = dto.CourseId,
                    SubjectId = dto.SubjectId,
                    DiaHoraDictado = "A confirmar"
                };

                await _enrollmentRepository.AddCourseSubjectAsync(newCourseSubject);
            }
            else if (!courseSubjectExists)
            {
                throw new ArgumentException($"La materia (SubjectId={dto.SubjectId}) no corresponde al año del curso (CourseId={dto.CourseId}).");
            }

            var existing = await _enrollmentRepository.GetAsync(dto.UserId, dto.CourseId, dto.SubjectId);
            if (existing != null) return;

            var entity = new UserCourseSubject
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId,
                SubjectId = dto.SubjectId,
                FechaInscripcion = dto.FechaInscripcion == default ? DateTime.Now : dto.FechaInscripcion
            };

            await _enrollmentRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(UserCourseSubjectDTO dto)
        {
            var entity = new UserCourseSubject
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId,
                SubjectId = dto.SubjectId,
                NotaFinal = dto.NotaFinal,
                FechaInscripcion = dto.FechaInscripcion
            };
            return await _enrollmentRepository.UpdateAsync(entity);
        }

        public async Task DeleteEnrollmentAsync(int userId, int courseId, int subjectId)
        {
            var existing = await _enrollmentRepository.GetAsync(userId, courseId, subjectId);
            if (existing == null)
                throw new InvalidOperationException("La inscripción no existe.");

            _enrollmentRepository.Delete(existing);
            await _enrollmentRepository.SaveChangesAsync();
        }

        private UserCourseSubjectDTO MapToDto(UserCourseSubject e)
        {
            return new UserCourseSubjectDTO
            {
                UserId = e.UserId,
                CourseId = e.CourseId,
                SubjectId = e.SubjectId,
                NotaFinal = e.NotaFinal,
                FechaInscripcion = e.FechaInscripcion ?? DateTime.MinValue
            };
        }
    }
}
