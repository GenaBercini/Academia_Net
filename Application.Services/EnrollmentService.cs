using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

using Shared.Types;


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

            var courseSubjectExists = await _enrollmentRepository.CourseSubjectExistsAsync(dto.CourseId, dto.SubjectId);
            if (!courseSubjectExists)
                throw new ArgumentException($"La materia (SubjectId={dto.SubjectId}) no está asociada al curso (CourseId={dto.CourseId}).");

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
