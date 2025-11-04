using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Shared.Types;

namespace Domain.Model
{

    public class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
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

        public ICollection<UserCourseSubject> CoursesSubjects { get; set; } = new List<UserCourseSubject>();

        public User(
        int id,
        string userName,
        string password,
        string name,
        string lastname,
        string email,
        string adress,
        UserType typeUser,
        string dni,
        string? studentNumber = null,
        JobPositionType? jobPosition = null,
        DateTime? dateOfAdmission = null,
        DateTime? dateOfHire = null)
        {
            SetId(id);
            SetUserName(userName);
            SetPassword(password);
            SetName(name);
            SetLastName(lastname);
            SetEmail(email);
            SetAdress(adress);
            SetTypeUser(typeUser);
            SetStatus(UserStatus.Active);
            SetDni(dni); 
            if(typeUser == UserType.Student && !(string.IsNullOrWhiteSpace(studentNumber)) && dateOfAdmission.HasValue)
            {
                SetStudentNumber(studentNumber);
                SetDateOfAdmission(dateOfAdmission.Value);
            }
            else if(typeUser == UserType.Teacher && jobPosition != null && dateOfHire.HasValue)
            {
                SetJobPosition(jobPosition.Value);
                SetDateOfHire(dateOfHire.Value);
            }
        }

        public User() { }

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

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(name));
            Name = name;
        }

        public void SetLastName(string lastname)
        {
            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("El apellido no puede ser nulo o vacío.", nameof(lastname));
            LastName = lastname;
        }

        public void SetStudentNumber(string studentNumber)
        {
            if (string.IsNullOrWhiteSpace(studentNumber)) {
                throw new ArgumentException("El numero de legajo no puede ser nulo o vacío.", nameof(studentNumber));
            }
            if (TypeUser != UserType.Student)
                throw new ArgumentException("Solo los estudiantes pueden tener número de estudiante.", nameof(studentNumber));
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

        public void SetJobPosition(JobPositionType jobPosition)
        {
            if (TypeUser != UserType.Teacher)
                throw new ArgumentException("Solo los docentes pueden tener un puesto de trabajo.", nameof(jobPosition));
            JobPosition = jobPosition;
        }

        public void SetDateOfAdmission(DateTime date)
        {
            if (TypeUser != UserType.Student)
                throw new InvalidOperationException("Solo los estudiantes pueden tener fecha de admisión.");

            if (date > DateTime.UtcNow)
                throw new ArgumentException("La fecha de admisión no puede ser futura.", nameof(date));

            DateOfAdmission = date;
        }

        public void SetDateOfHire(DateTime date)
        {
            if (TypeUser != UserType.Teacher)
                throw new ArgumentException("Solo los docentes pueden tener fecha de contratación.", nameof(date));

            if (date > DateTime.UtcNow)
                throw new ArgumentException("La fecha de contratación no puede ser futura.", nameof(date));

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

        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
