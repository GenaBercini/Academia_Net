using Data;
using Domain.Model;
using DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class UserService 
    {
        public UserDTO Add(UserCreateDTO createDto)
        {
            var userRepository = new UserRepository();

            User user = new(0,
                createDto.UserName,
                createDto.Password,
                createDto.Name,
                createDto.LastName,
                createDto.Email,
                createDto.Adress,
                createDto.TypeUser,
                createDto.Dni,
                createDto.TypeUser == UserType.Student ? createDto.StudentNumber : null,
                createDto.TypeUser == UserType.Teacher ? createDto.JobPosition : null,
                createDto.TypeUser == UserType.Student ? createDto.DateOfAdmission : null,
                createDto.TypeUser == UserType.Teacher ? createDto.DateOfHire : null
            );

            if (!string.IsNullOrWhiteSpace(createDto.Dni))
                user.SetDni(createDto.Dni);

            if (createDto.DateOfAdmission.HasValue && createDto.TypeUser == UserType.Student)
                user.SetDateOfAdmission(createDto.DateOfAdmission.Value);

            if (createDto.DateOfHire.HasValue && createDto.TypeUser == UserType.Teacher)
                user.SetDateOfHire(createDto.DateOfHire.Value);

            if (createDto.JobPosition.HasValue && createDto.TypeUser == UserType.Teacher)
                user.SetJobPosition(createDto.JobPosition.Value);

            if (!string.IsNullOrWhiteSpace(createDto.StudentNumber) && createDto.TypeUser == UserType.Student)
                user.SetStudentNumber(createDto.StudentNumber);

            userRepository.Add(user);

            return MapToDTO(user);
        }

        public bool Delete(int id)
        {
            var userRepository = new UserRepository();
            return userRepository.Delete(id);
        }

        public UserDTO? Get(int id)
        {
            var userRepository = new UserRepository();
            User? user = userRepository.Get(id);

            if (user == null)
                return null;

            return MapToDTO(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var userRepository = new UserRepository();
            var usuarios = userRepository.GetAll();

            return usuarios.Select(user => MapToDTO(user));
        }
          public bool Update(UserUpdateDTO updateDto)
        {
            var userRepository = new UserRepository();
            var usuario = userRepository.Get(updateDto.Id);
            if (usuario == null)
                return false;

            usuario.SetUserName(updateDto.UserName);
            usuario.SetName(updateDto.Name);
            usuario.SetLastName(updateDto.LastName);
            usuario.SetEmail(updateDto.Email);
            usuario.SetStatus(updateDto.Status);
            usuario.SetTypeUser(updateDto.TypeUser);

            if (!string.IsNullOrWhiteSpace(updateDto.Dni))
                usuario.SetDni(updateDto.Dni);

            if (!string.IsNullOrWhiteSpace(updateDto.StudentNumber))
                usuario.SetStudentNumber(updateDto.StudentNumber);

            if (!string.IsNullOrWhiteSpace(updateDto.Adress))
                usuario.SetAdress(updateDto.Adress);

            if (updateDto.DateOfAdmission.HasValue)
                usuario.SetDateOfAdmission(updateDto.DateOfAdmission.Value);

            if (updateDto.DateOfHire.HasValue)
                usuario.SetDateOfHire(updateDto.DateOfHire.Value);

            if (updateDto.JobPosition.HasValue)
                usuario.SetJobPosition(updateDto.JobPosition.Value);

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
                usuario.SetPassword(updateDto.Password);

            return userRepository.Update(usuario);
        }
        private UserDTO MapToDTO(User user) => new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Dni = user.Dni,
            StudentNumber = user.StudentNumber,
            Adress = user.Adress,
            TypeUser = user.TypeUser,
            JobPosition = user.JobPosition,
            DateOfAdmission = user.DateOfAdmission,
            DateOfHire = user.DateOfHire,
            Status = user.Status
        };
    }
}
