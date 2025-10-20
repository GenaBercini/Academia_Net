using Domain.Model;
using DTOs;

namespace Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(User user) => new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            LastName = user.Lastname,
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

        public static User ToEntity(UserCreateDTO dto)
        {
            return new User(
                id: 0,
                userName: dto.UserName,
                password: dto.Password,
                name: dto.Name,
                lastname: dto.LastName,
                email: dto.Email,
                adress: dto.Adress,
                typeUser: dto.TypeUser,
                dni: dto.Dni,
                studentNumber: dto.StudentNumber,
                jobPosition: dto.JobPosition,
                dateOfAdmission: dto.DateOfAdmission,
                dateOfHire: dto.DateOfHire
            );
        }
    }

}
