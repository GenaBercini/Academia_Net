using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Types;


namespace Application.Services
{
    public class EnrollmentService
    {
        private readonly UserCourseSubjectRepository _userCourseSubjectRepository;
        private readonly UserRepository _userRepository;

        public EnrollmentService(UserRepository userRepository, UserCourseSubjectRepository userCourseSubjectRepository)
        {
            _userRepository = userRepository;
            _userCourseSubjectRepository = userCourseSubjectRepository;
        }

        public async Task<bool> EnrollUserInCourseSubject(int userId, int courseId, int subjectId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null || user.Status != UserStatus.Active)
                throw new InvalidOperationException("Usuario no encontrado o inactivo.");

            // permite tanto docentes como estudiantes
            if (user.TypeUser != UserType.Student && user.TypeUser != UserType.Teacher)
                throw new InvalidOperationException("Solo docentes o estudiantes pueden inscribirse en materias.");

            // Evitar duplicados
            var existing = await _userCourseSubjectRepository.GetAsync(userId, courseId, subjectId);
            if (existing != null)
                throw new InvalidOperationException("El usuario ya está inscripto en esta materia.");
            var enrollment = new UserCourseSubject
            {
                UserId = userId,
                CourseId = courseId,
                SubjectId = subjectId,
                FechaInscripcion = DateTime.Now
            };

            await _userCourseSubjectRepository.AddAsync(enrollment);
            return true;
        }

        public IEnumerable<UserCourseSubjectDTO> GetEnrollmentsByUser(int userId)
        {
            var enrollments = _userCourseSubjectRepository.GetByUser(userId);
            return enrollments.Select(MapToDto).ToList();
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
