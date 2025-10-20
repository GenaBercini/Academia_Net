using Domain.Model;

namespace DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string? StudentNumber { get; set; }
        public string Adress { get; set; }
        public UserType TypeUser { get; set; }
        public JobPositionType? JobPosition { get; set; }
        public DateTime? DateOfAdmission { get; set; }
        public DateTime? DateOfHire { get; set; }
        public UserStatus Status { get; set; }
    }

    public class UserCreateDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string? StudentNumber { get; set; }
        public string Adress { get; set; }
        public UserType TypeUser { get; set; }
        public JobPositionType? JobPosition { get; set; }
        public DateTime? DateOfAdmission { get; set; }
        public DateTime? DateOfHire { get; set; }

        public string Password { get; set; } = string.Empty;
    }

    public class UserUpdateDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string? StudentNumber { get; set; }
        public string Adress { get; set; }
        public UserType TypeUser { get; set; }
        public JobPositionType? JobPosition { get; set; }
        public DateTime? DateOfAdmission { get; set; }
        public DateTime? DateOfHire { get; set; }
        public UserStatus Status { get; set; }

        public string? Password { get; set; }
    }
}
