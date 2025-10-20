using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Domain.Model
{
    public enum UserType
    {
        Admin = 1,
        Student = 2,
        Teacher = 3
    }

    public enum JobPositionType
    {
        Practice = 1,
        Theory = 2,
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }

    public class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public string Email { get; private set; }
        public string Dni { get; private set; }
        public string? StudentNumber { get; private set; }          
        public string Adress { get; private set; }
        public UserType TypeUser { get; private set; }
        public JobPositionType? JobPosition { get; private set; }   
        public DateTime? DateOfAdmission { get; private set; }     
        public DateTime? DateOfHire { get; private set; }           
        public UserStatus Status { get; private set; }

        public User(int id, string userName, string password, string nombre, string apellido, string email, string adress, UserType typeUser)
        {
            SetId(id);
            SetUserName(userName);
            SetPassword(password);
            SetNombre(nombre);
            SetApellido(apellido);
            SetEmail(email);
            SetStatus(UserStatus.Active);
            SetTypeUser(typeUser);
            SetAdress(adress);
        }

        public User(int id, string userName, string nombre, string apellido, string email, string adress, UserType typeUser, string salt, string passwordHash)
        {
            Id = id;
            UserName = userName;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Adress = adress;
            TypeUser = typeUser;
            Status = UserStatus.Active;
            Salt = salt;
            PasswordHash = passwordHash;
        }

        public static User CreateAdminSeed(int id, string userName, string nombre, string apellido, string email, string adress, UserType typeUser, string password)
        {
            return new User(id, userName, password, nombre, apellido, email, adress, typeUser);
        }

        public void SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(userName));
            UserName = userName;
        }

        public void SetStatus(UserStatus status)
        {
            Status = status;
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI no puede ser nulo o vacío.", nameof(dni));
            Dni = dni;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede ser nulo o vacío.", nameof(apellido));
            Apellido = apellido;
        }

        public void SetStudentNumber(string? studentNumber)
        {
            if (TypeUser != UserType.Student)
                throw new InvalidOperationException("Solo los estudiantes pueden tener número de estudiante.");
            StudentNumber = string.IsNullOrWhiteSpace(studentNumber) ? null : studentNumber;
        }

        public void SetAdress(string adress)
        {
            if (string.IsNullOrWhiteSpace(adress))
                throw new ArgumentException("La dirección no puede ser nula o vacía.", nameof(adress));
            Adress = adress;
        }

        public void SetTypeUser(UserType typeUser)
        {
            if (!Enum.IsDefined(typeof(UserType), typeUser))
                throw new ArgumentException("El tipo de usuario no es válido.");
            TypeUser = typeUser;
        }

        public void SetJobPosition(JobPositionType? jobPosition)
        {
            if (TypeUser != UserType.Teacher)
                throw new InvalidOperationException("Solo los docentes pueden tener un puesto de trabajo.");

            if (jobPosition.HasValue && !Enum.IsDefined(typeof(JobPositionType), jobPosition.Value))
                throw new ArgumentException("El puesto de trabajo no es válido.");

            JobPosition = jobPosition;
        }

        public void SetDateOfAdmission(DateTime? date)
        {
            if (TypeUser != UserType.Student)
                throw new InvalidOperationException("Solo los estudiantes pueden tener fecha de admisión.");

            if (date.HasValue && date.Value > DateTime.UtcNow)
                throw new ArgumentException("La fecha de admisión no puede ser futura.");

            DateOfAdmission = date;
        }

        public void SetDateOfHire(DateTime? date)
        {
            if (TypeUser != UserType.Teacher)
                throw new InvalidOperationException("Solo los docentes pueden tener fecha de contratación.");

            if (date.HasValue && date.Value > DateTime.UtcNow)
                throw new ArgumentException("La fecha de contratación no puede ser futura.");

            DateOfHire = date;
        }

        public void SetEmail(string email)
        {
            if (!EsEmailValido(email))
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(password));

            if (password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.", nameof(password));

            Salt = GenerateSalt();
            PasswordHash = HashPassword(password, Salt);
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string hashedInput = HashPassword(password, Salt);
            return PasswordHash == hashedInput;
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
