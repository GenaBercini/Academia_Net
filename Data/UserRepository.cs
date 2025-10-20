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
    }
}