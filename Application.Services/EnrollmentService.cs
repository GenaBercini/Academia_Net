using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class EnrollmentService
    {
        private readonly UserRepository userRepository;

        public EnrollmentService()
        {
            userRepository = new UserRepository();
        }

        public async Task<bool> EnrollUserInCourseSubject(int userId, int courseId, int subjectId)
        {
            var user = await userRepository.GetAsync(userId);
            if (user == null || user.Status != UserStatus.Active)
                throw new InvalidOperationException("Usuario no encontrado o inactivo.");

            if (user.TypeUser != UserType.Student)
                throw new InvalidOperationException("Solo los estudiantes pueden inscribirse en materias.");

            var enrollment = userRepository.EnrollUserInCourseSubject(userId, courseId, subjectId);
            if (enrollment == null)
                throw new InvalidOperationException("No se pudo crear la inscripción.");

            return enrollment != null;
        }

        public IEnumerable<UserCourseSubjectDTO> GetEnrollmentsByUser(int userId)
        {
            var enrollments = userRepository.GetEnrollmentsByUser(userId);
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
                FechaInscripcion = e.FechaInscripcion.Value,
            };
        }
    }
}