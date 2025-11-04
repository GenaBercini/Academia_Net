using Domain.Model;
using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Types;
using System.Drawing;
using System.Reflection.Metadata;

namespace Data
{
    public class UserRepository
    {
        private readonly TPIContext _context;

        public UserRepository(TPIContext context)
        {
            _context = context;
        }
        public UserCourseSubject? EnrollUserInCourseSubject(int userId, int courseId, int subjectId)
        {

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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
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
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public  User? GetByUsername(string username)
        {
            return  _context.Users.FirstOrDefault(u => u.UserName == username && u.Status == UserStatus.Active);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => u.Status == UserStatus.Active)
                .ToListAsync();
        }
       
        public async Task<bool> UpdateAsync(User user)
        {
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
            return _context.UsersCoursesSubjects.Any(e =>
                e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            var existingUsuario = await _context.Users.FindAsync(userId);
            if (existingUsuario == null)
                return false;

            existingUsuario.SetPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllADOAsync(bool onlyStudents = true)
        {
            const string sql = @"
            SELECT Id, UserName, Name, LastName, Email, Dni, StudentNumber, Adress,
                TypeUser, JobPosition, DateOfAdmission, DateOfHire, Status
            FROM Users
            WHERE Status = @Status
            /**onlyStudents**/
            ORDER BY Id";

            var users = new List<User>();
            string connectionString = _context.Database.GetConnectionString();
            var query = sql.Replace("/**onlyStudents**/", onlyStudents ? "AND TypeUser = @TypeUser" : "");

            await using var connection = new SqlConnection(connectionString);
            await using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Status", (int)UserStatus.Active);
            if (onlyStudents)
                command.Parameters.AddWithValue("@TypeUser", (int)UserType.Student);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            var oId = reader.GetOrdinal("Id");
            var oUserName = reader.GetOrdinal("UserName");
            var oName = reader.GetOrdinal("Name");
            var oLastName = reader.GetOrdinal("LastName");
            var oEmail = reader.GetOrdinal("Email");
            var oDni = reader.GetOrdinal("Dni");
            var oStudentNumber = reader.GetOrdinal("StudentNumber");
            var oAdress = reader.GetOrdinal("Adress");
            var oTypeUser = reader.GetOrdinal("TypeUser");
            var oJobPosition = reader.GetOrdinal("JobPosition");
            var oDateOfAdmission = reader.GetOrdinal("DateOfAdmission");
            var oDateOfHire = reader.GetOrdinal("DateOfHire");
            var oStatus = reader.GetOrdinal("Status");

            while (await reader.ReadAsync())
            {
                object Get(int ord) => reader.IsDBNull(ord) ? DBNull.Value : reader.GetValue(ord);

                int id = Convert.ToInt32(Get(oId));
                var userName = Get(oUserName) == DBNull.Value ? null : (string)Get(oUserName);
                var name = Get(oName) == DBNull.Value ? null : (string)Get(oName);
                var lastName = Get(oLastName) == DBNull.Value ? null : (string)Get(oLastName);
                var email = Get(oEmail) == DBNull.Value ? null : (string)Get(oEmail);
                var dni = Get(oDni) == DBNull.Value ? null : (string)Get(oDni);
                var studentNumber = Get(oStudentNumber) == DBNull.Value ? null : (string)Get(oStudentNumber);
                var adress = Get(oAdress) == DBNull.Value ? null : (string)Get(oAdress);

                var rawType = Get(oTypeUser);
                int typeUserInt = rawType == DBNull.Value ? (int)UserType.Student : Convert.ToInt32(rawType);

                var rawJob = Get(oJobPosition);
                int? jobPosInt = rawJob == DBNull.Value ? (int?)null : Convert.ToInt32(rawJob);

                var rawDateAdm = Get(oDateOfAdmission);
                DateTime? dateOfAdmission = rawDateAdm == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rawDateAdm);

                var rawDateHire = Get(oDateOfHire);
                DateTime? dateOfHire = rawDateHire == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rawDateHire);

                var rawStatus = Get(oStatus);
                int statusInt = rawStatus == DBNull.Value ? (int)UserStatus.Active : Convert.ToInt32(rawStatus);

                var user = new User();
                user.SetId(id);

                user.SetTypeUser((UserType)typeUserInt);

                if (!string.IsNullOrWhiteSpace(userName)) user.SetUserName(userName!);
                if (!string.IsNullOrWhiteSpace(name)) user.SetName(name!);
                if (!string.IsNullOrWhiteSpace(lastName)) user.SetLastName(lastName!);
                if (!string.IsNullOrWhiteSpace(email)) user.SetEmail(email!);
                if (!string.IsNullOrWhiteSpace(adress)) user.SetAdress(adress!);
                if (!string.IsNullOrWhiteSpace(dni)) user.SetDni(dni!);

                if (!string.IsNullOrWhiteSpace(studentNumber) && user.TypeUser == UserType.Student)
                    user.SetStudentNumber(studentNumber!);

                if (dateOfAdmission.HasValue && user.TypeUser == UserType.Student)
                    user.SetDateOfAdmission(dateOfAdmission.Value);

                if (jobPosInt.HasValue && user.TypeUser == UserType.Teacher)
                    user.SetJobPosition((JobPositionType)jobPosInt.Value);

                if (dateOfHire.HasValue && user.TypeUser == UserType.Teacher)
                    user.SetDateOfHire(dateOfHire.Value);

                user.SetStatus((UserStatus)statusInt);

                users.Add(user);
            }

            return users;
        }
    }
}