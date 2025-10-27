using Domain.Model;
using DTOs;
using Microsoft.EntityFrameworkCore;

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