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
                createDto.Nombre,
                createDto.Apellido,
                createDto.Email,
                createDto.Adress,
                createDto.TypeUser
            );

            if (!string.IsNullOrWhiteSpace(createDto.Dni))
                user.SetDni(createDto.Dni);

            if (createDto.DateOfAdmission.HasValue && createDto.TypeUser == UserType.Student)
                user.SetDateOfAdmission(createDto.DateOfAdmission);

            if (createDto.DateOfHire.HasValue && createDto.TypeUser == UserType.Teacher)
                user.SetDateOfHire(createDto.DateOfHire);

            if (createDto.JobPosition.HasValue && createDto.TypeUser == UserType.Teacher)
                user.SetJobPosition(createDto.JobPosition);
            if (!string.IsNullOrWhiteSpace(createDto.StudentNumber) && createDto.TypeUser == UserType.Teacher)
                user.SetStudentNumber(createDto.StudentNumber); 

            user.SetAdress(createDto.Adress);               

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
            usuario.SetNombre(updateDto.Nombre);
            usuario.SetApellido(updateDto.Apellido);
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
                usuario.SetDateOfAdmission(updateDto.DateOfAdmission);

            if (updateDto.DateOfHire.HasValue)
                usuario.SetDateOfHire(updateDto.DateOfHire);

            if (updateDto.JobPosition.HasValue)
                usuario.SetJobPosition(updateDto.JobPosition);

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
                usuario.SetPassword(updateDto.Password);

            return userRepository.Update(usuario);
        }
        private UserDTO MapToDTO(User user) => new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Nombre = user.Nombre,
            Apellido = user.Apellido,
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
