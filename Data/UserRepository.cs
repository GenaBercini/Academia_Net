using Domain.Model;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public UserCourseSubject? EnrollUserInCourseSubject(int userId, int courseId, int subjectId)
        {
            using var context = CreateContext();

            var user = context.Users.FirstOrDefault(u => u.Id == userId && u.Status == Domain.Model.UserStatus.Active);
            if (user == null) throw new InvalidOperationException("Usuario no encontrado o inactivo.");

            var course = context.Courses.Include(c => c.CoursesSubjects)
                                        .FirstOrDefault(c => c.Id == courseId && !c.IsDeleted);
            if (course == null) throw new InvalidOperationException("Curso no encontrado.");

            var subject = context.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null) throw new InvalidOperationException("Materia no encontrada.");

            var courseSubject = context.CoursesSubjects
                                       .Include(cs => cs.Course)
                                       .Include(cs => cs.Subject)
                                       .FirstOrDefault(cs => cs.CourseId == courseId && cs.SubjectId == subjectId);

            if (courseSubject == null) throw new InvalidOperationException("La materia no pertenece al curso seleccionado.");

            var existing = context.UsersCoursesSubjects
                                  .Include(e => e.CourseSubject)
                                  .ThenInclude(cs => cs.Course)
                                  .Include(e => e.CourseSubject)
                                  .ThenInclude(cs => cs.Subject)
                                  .FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
            if (existing != null) return existing;

            if (course.Cupo > 0)
            {
                int enrolledCount = context.UsersCoursesSubjects.Count(e => e.CourseId == courseId && e.SubjectId == subjectId);
                if (enrolledCount >= course.Cupo) throw new InvalidOperationException("El curso no tiene cupo disponible.");
            }

            var enrollment = new UserCourseSubject
            {
                UserId = userId,
                CourseId = courseId,
                SubjectId = subjectId,
                CourseSubject = courseSubject,
                FechaInscripcion = DateTime.UtcNow
            };

            context.UsersCoursesSubjects.Add(enrollment);
            context.SaveChanges();

            var created = context.UsersCoursesSubjects
                                 .Include(e => e.CourseSubject)
                                 .ThenInclude(cs => cs.Course)
                                 .Include(e => e.CourseSubject)
                                 .ThenInclude(cs => cs.Subject)
                                 .FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
            return created;
        }

        public IEnumerable<UserCourseSubject> GetEnrollmentsByUser(int userId)
        {
            using var context = CreateContext();
            return context.UsersCoursesSubjects
                          .Include(e => e.CourseSubject)
                          .ThenInclude(cs => cs.Course)
                          .Include(e => e.CourseSubject)
                          .ThenInclude(cs => cs.Subject)
                          .Where(e => e.UserId == userId)
                          .ToList();
        }

        public void Add(User user)
        {
            using var context = CreateContext();
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var usuario = context.Users.Find(id);
            if (usuario != null)
            {
                usuario.SetStatus(UserStatus.Deleted);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public User? Get(int id)
        {
            using var context = CreateContext();
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            using var context = CreateContext();
            return context.Users.FirstOrDefault(u => u.UserName == username && u.Status == UserStatus.Active);
        }

        public IEnumerable<User> GetAll()
        {
            using var context = CreateContext();
            return context.Users.Where(u => u.Status == UserStatus.Active).ToList();
        }

        public bool Update(User user)
        {
            using var context = CreateContext();
            var existingUsuario = context.Users.Find(user.Id);
            if (existingUsuario != null)
            {
                existingUsuario.SetUserName(user.UserName);
                existingUsuario.SetName(user.Name);
                existingUsuario.SetLastName(user.LastName);
                existingUsuario.SetEmail(user.Email);
                existingUsuario.SetStatus(user.Status);
                existingUsuario.SetTypeUser(user.TypeUser);

                if (!string.IsNullOrWhiteSpace(user.Dni))
                    existingUsuario.SetDni(user.Dni);

                if (!string.IsNullOrWhiteSpace(user.Adress))
                    existingUsuario.SetAdress(user.Adress);

                if (!string.IsNullOrWhiteSpace(user.StudentNumber) && user.TypeUser == UserType.Student)
                    existingUsuario.SetStudentNumber(user.StudentNumber);

                if (user.DateOfAdmission.HasValue && user.TypeUser == UserType.Student)
                    existingUsuario.SetDateOfAdmission(user.DateOfAdmission.Value);

                if (user.DateOfHire.HasValue && user.TypeUser == UserType.Teacher)
                    existingUsuario.SetDateOfHire(user.DateOfHire.Value);

                if (user.JobPosition.HasValue && user.TypeUser == UserType.Teacher)
                    existingUsuario.SetJobPosition(user.JobPosition.Value);

                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsUserEnrolled(int userId, int courseId, int subjectId)
        {
            using var context = CreateContext();
            return context.UsersCoursesSubjects.Any(e =>
                e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
        }
    }
}