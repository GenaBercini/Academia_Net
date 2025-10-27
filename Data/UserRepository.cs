using Domain.Model;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Shared.Types;

namespace Data
{
    public class UserRepository
    {
        //private TPIContext CreateContext()
        //{
        //    return new TPIContext();
        //}

        private readonly TPIContext _context;

        public UserRepository(TPIContext context)
        {
            _context = context;
        }
        public UserCourseSubject? EnrollUserInCourseSubject(int userId, int courseId, int subjectId)
        {
            //using var context = CreateContext();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId && u.Status == UserStatus.Active);
            if (user == null) throw new InvalidOperationException("Usuario no encontrado o inactivo.");

            var course = _context.Courses.Include(c => c.CoursesSubjects)
                                        .FirstOrDefault(c => c.Id == courseId && !c.IsDeleted);
            if (course == null) throw new InvalidOperationException("Curso no encontrado.");

            var subject = _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null) throw new InvalidOperationException("Materia no encontrada.");

            var courseSubject = _context.CoursesSubjects
                                       .Include(cs => cs.Course)
                                       .Include(cs => cs.Subject)
                                       .FirstOrDefault(cs => cs.CourseId == courseId && cs.SubjectId == subjectId);

            if (courseSubject == null) throw new InvalidOperationException("La materia no pertenece al curso seleccionado.");

            var existing = _context.UsersCoursesSubjects
                                  .Include(e => e.CourseSubject)
                                  .ThenInclude(cs => cs.Course)
                                  .Include(e => e.CourseSubject)
                                  .ThenInclude(cs => cs.Subject)
                                  .FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
            if (existing != null) return existing;

            if (course.Cupo > 0)
            {
                int enrolledCount = _context.UsersCoursesSubjects.Count(e => e.CourseId == courseId && e.SubjectId == subjectId);
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

            _context.UsersCoursesSubjects.Add(enrollment);
            _context.SaveChanges();

            var created = _context.UsersCoursesSubjects
                                 .Include(e => e.CourseSubject)
                                 .ThenInclude(cs => cs.Course)
                                 .Include(e => e.CourseSubject)
                                 .ThenInclude(cs => cs.Subject)
                                 .FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
            return created;
        }

        public IEnumerable<UserCourseSubject> GetEnrollmentsByUser(int userId)
        {
            //using var context = CreateContext();
            return _context.UsersCoursesSubjects
                          .Include(e => e.CourseSubject)
                          .ThenInclude(cs => cs.Course)
                          .Include(e => e.CourseSubject)
                          .ThenInclude(cs => cs.Subject)
                          .Where(e => e.UserId == userId)
                          .ToList();
        }
        public async Task AddAsync(User user)
        {
            //using var context = CreateContext();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //using var context = CreateContext();
            var usuario = await _context.Users.FindAsync(id);
            if (usuario != null)
            {
                usuario.SetStatus(UserStatus.Deleted);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<User?> GetAsync(int id)
        {
            //using var context = CreateContext();
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public  User? GetByUsername(string username)
        {
            //using var _context = CreateContext();
            return  _context.Users.FirstOrDefault(u => u.UserName == username && u.Status == UserStatus.Active);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            //using var _context = CreateContext();
            return await _context.Users
                .Where(u => u.Status == UserStatus.Active)
                .ToListAsync();
        }
       
        public async Task<bool> UpdateAsync(User user)
        {
            //using var context = CreateContext();
            var existingUsuario = await _context.Users.FindAsync(user.Id);
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

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public bool IsUserEnrolled(int userId, int courseId, int subjectId)
        {
            //using var context = CreateContext();
            return _context.UsersCoursesSubjects.Any(e =>
                e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
        }
    }
}