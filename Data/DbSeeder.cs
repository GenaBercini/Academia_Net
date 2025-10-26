//using Domain.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Data
//{
//    public static class DbSeeder
//    {
//        public static void Initialize(TPIContext context)
//        {
//            if (context == null) throw new ArgumentNullException(nameof(context));

//            using var tx = context.Database.BeginTransaction();
//            try
//            {
//                // 1) Specialties
//                if (!context.Specialties.Any())
//                {
//                    var s1 = new Specialty(0, "Informática", 5);
//                    var s2 = new Specialty(0, "Administración", 4);
//                    context.Specialties.AddRange(s1, s2);
//                    context.SaveChanges();
//                }

//                // 2) Plans
//                if (!context.Plans.Any())
//                {
//                    var inf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
//                    var adm = context.Specialties.First(s => s.DescEspecialidad == "Administración");

//                    var p1 = new Plan(0, "Plan 2024 - Informática", 2024) { SpecialtyId = inf.Id };
//                    var p2 = new Plan(0, "Plan 2024 - Administración", 2024) { SpecialtyId = adm.Id };

//                    context.Plans.AddRange(p1, p2);
//                    context.SaveChanges();
//                }

//                // 3) Subjects
//                if (!context.Subjects.Any())
//                {
//                    var planInf = context.Plans.First(p => p.Descripcion.Contains("Informática"));
//                    var planAdm = context.Plans.First(p => p.Descripcion.Contains("Administración"));

//                    var subj1 = new Subject(0, "Programación I", 6, true) { PlanId = planInf.Id };
//                    var subj2 = new Subject(0, "Base de Datos", 4, true) { PlanId = planInf.Id };
//                    var subj3 = new Subject(0, "Economía", 4, true) { PlanId = planAdm.Id };

//                    context.Subjects.AddRange(subj1, subj2, subj3);
//                    context.SaveChanges();
//                }

//                // 4) Courses
//                if (!context.Courses.Any())
//                {
//                    var specInf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
//                    var specAdm = context.Specialties.First(s => s.DescEspecialidad == "Administración");

//                    var c1 = new Course(0, 30, DateTime.Now.Year, "Tarde", "A") { SpecialtyId = specInf.Id };
//                    var c2 = new Course(0, 25, DateTime.Now.Year, "Mañana", "B") { SpecialtyId = specAdm.Id };

//                    context.Courses.AddRange(c1, c2);
//                    context.SaveChanges();
//                }

//                // 5) CourseSubjects (relación Curso-Materia)
//                if (!context.CoursesSubjects.Any())
//                {
//                    var course1 = context.Courses.First();
//                    var course2 = context.Courses.Skip(1).FirstOrDefault();
//                    var subjProg = context.Subjects.First(s => s.Desc.Contains("Programación"));
//                    var subjBD = context.Subjects.First(s => s.Desc.Contains("Base de Datos"));

//                    var cs1 = new CourseSubject { CourseId = course1.Id, SubjectId = subjProg.Id, DiaHoraDictado = "Lun 18-20" };
//                    var cs2 = new CourseSubject { CourseId = course2.Id, SubjectId = subjBD.Id, DiaHoraDictado = "Mar 19-21" };

//                    context.CoursesSubjects.AddRange(cs1, cs2);
//                    context.SaveChanges();
//                }

//                if (!context.Users.Any(u => u.TypeUser == UserType.Admin))
//                {
//                    var admin = new User(0, "admin", "admin123", "Juan", "Admin", "admin@tpi.com", "Juan Jose Paso 123", Domain.Model.UserType.Admin, "42789654");
//                    context.Users.Add(admin);
//                    context.SaveChanges();
//                }

//                // 6) Usuarios (estudiantes de ejemplo)
//                if (!context.Users.Any(u => u.TypeUser == UserType.Student))
//                {
//                    var student1 = new User(0, "estudiante1", "Password123!", "Ana", "Gómez", "ana.gomez@example.com", "C/ Falsa 123", UserType.Student, "30123456", "L-1001");
//                    var student2 = new User(0, "estudiante2", "Password123!", "Luis", "Pérez", "luis.perez@example.com", "C/ Verdadera 456", UserType.Student, "40123456", "L-1002");

//                    context.Users.AddRange(student1, student2);
//                    context.SaveChanges();
//                }

//                // 7) Inscripciones / Notas
//                if (!context.UsersCoursesSubjects.Any())
//                {
//                    var alumno = context.Users.First(u => u.UserName == "estudiante1");
//                    var cs = context.CoursesSubjects.First();
//                    var enrollment = new UserCourseSubject
//                    {
//                        UserId = alumno.Id,
//                        CourseId = cs.CourseId,
//                        SubjectId = cs.SubjectId,
//                        FechaInscripcion = DateTime.UtcNow,
//                        NotaFinal = 8.5m
//                    };
//                    context.UsersCoursesSubjects.Add(enrollment);
//                    context.SaveChanges();
//                }

//                tx.Commit();
//            }
//            catch
//            {
//                tx.Rollback();
//                throw;
//            }
//        }
//    }
//}

//using Domain.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Cryptography; // Para generar Salt y PasswordHash
//using static System.Runtime.InteropServices.JavaScript.JSType; // No es necesario, solo para ejemplo de Password

//namespace Data
//{
//    public static class DbSeeder
//    {
//        public static void Initialize(TPIContext context)
//        {
//            if (context == null) throw new ArgumentNullException(nameof(context));

//            using var tx = context.Database.BeginTransaction();
//            try
//            {
//                // Helper para generar hash y salt de contraseñas
//                // Nota: En una app real, esto estaría en un servicio de autenticación
//                // y usarías una librería como BCrypt.NET para mayor seguridad.
//                // Aquí es simplificado solo para el seeder.
//                (string hash, string salt) GeneratePasswordHash(string password)
//                {
//                    using (var hmac = new HMACSHA512())
//                    {
//                        var generatedSalt = hmac.Key;
//                        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//                        return (Convert.ToBase64String(computedHash), Convert.ToBase64String(generatedSalt));
//                    }
//                }

//                // 1) Specialties
//                if (!context.Specialties.Any())
//                {
//                    Console.WriteLine("Seed: Agregando especialidades...");
//                    var s1 = new Specialty(0, "Informática", 5);
//                    var s2 = new Specialty(0, "Administración", 4);
//                    var s3 = new Specialty(0, "Contabilidad", 4);
//                    var s4 = new Specialty(0, "Recursos Humanos", 3);

//                    context.Specialties.AddRange(s1, s2, s3, s4);
//                    context.SaveChanges();
//                }

//                // Obtener especialidades para usar en otros seeds
//                var specInf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
//                var specAdm = context.Specialties.First(s => s.DescEspecialidad == "Administración");
//                var specCont = context.Specialties.First(s => s.DescEspecialidad == "Contabilidad");
//                var specRRHH = context.Specialties.First(s => s.DescEspecialidad == "Recursos Humanos");


//                // 2) Plans
//                if (!context.Plans.Any())
//                {
//                    Console.WriteLine("Seed: Agregando planes...");
//                    var p1 = new Plan(0, "Plan 2024 - Informática", 2024) { SpecialtyId = specInf.Id };
//                    var p2 = new Plan(0, "Plan 2024 - Administración", 2024) { SpecialtyId = specAdm.Id };
//                    var p3 = new Plan(0, "Plan 2023 - Contabilidad", 2023) { SpecialtyId = specCont.Id };
//                    var p4 = new Plan(0, "Plan 2025 - Recursos Humanos", 2025) { SpecialtyId = specRRHH.Id };
//                    var p5 = new Plan(0, "Plan 2020 - Informática (Obsoleto)", 2020) { SpecialtyId = specInf.Id, IsDeleted = true };


//                    context.Plans.AddRange(p1, p2, p3, p4, p5);
//                    context.SaveChanges();
//                }

//                // Obtener planes para usar en otros seeds
//                var planInf24 = context.Plans.First(p => p.Descripcion.Contains("Informática") && p.Año_calendario == 2024);
//                var planAdm24 = context.Plans.First(p => p.Descripcion.Contains("Administración") && p.Año_calendario == 2024);
//                var planCont23 = context.Plans.First(p => p.Descripcion.Contains("Contabilidad") && p.Año_calendario == 2023);
//                var planRRHH25 = context.Plans.First(p => p.Descripcion.Contains("Recursos Humanos") && p.Año_calendario == 2025);


//                // 3) Subjects
//                if (!context.Subjects.Any())
//                {
//                    Console.WriteLine("Seed: Agregando materias...");
//                    var subj1 = new Subject(0, "Programación I", 6, true) { PlanId = planInf24.Id, Año = 1 };
//                    var subj2 = new Subject(0, "Base de Datos I", 4, true) { PlanId = planInf24.Id, Año = 2 };
//                    var subj3 = new Subject(0, "Estructuras de Datos", 5, true) { PlanId = planInf24.Id, Año = 2 };
//                    var subj4 = new Subject(0, "Redes de Computadoras", 4, true) { PlanId = planInf24.Id, Año = 3 };

//                    var subj5 = new Subject(0, "Economía General", 4, true) { PlanId = planAdm24.Id, Año = 1 };
//                    var subj6 = new Subject(0, "Marketing Estratégico", 3, true) { PlanId = planAdm24.Id, Año = 2 };
//                    var subj7 = new Subject(0, "Contabilidad Financiera", 5, true) { PlanId = planCont23.Id, Año = 1 };
//                    var subj8 = new Subject(0, "Derecho Laboral", 3, true) { PlanId = planRRHH25.Id, Año = 2 };
//                    var subj9 = new Subject(0, "Inglés Técnico", 2, false) { PlanId = planInf24.Id, Año = 1 };

//                    context.Subjects.AddRange(subj1, subj2, subj3, subj4, subj5, subj6, subj7, subj8, subj9);
//                    context.SaveChanges();
//                }

//                // Obtener materias para usar en otros seeds
//                var subjProgI = context.Subjects.First(s => s.Desc == "Programación I");
//                var subjBD1 = context.Subjects.First(s => s.Desc == "Base de Datos I");
//                var subjED = context.Subjects.First(s => s.Desc == "Estructuras de Datos");
//                var subjRedes = context.Subjects.First(s => s.Desc == "Redes de Computadoras");
//                var subjEco = context.Subjects.First(s => s.Desc == "Economía General");
//                var subjMark = context.Subjects.First(s => s.Desc == "Marketing Estratégico");
//                var subjContFin = context.Subjects.First(s => s.Desc == "Contabilidad Financiera");
//                var subjDerLab = context.Subjects.First(s => s.Desc == "Derecho Laboral");
//                var subjIngles = context.Subjects.First(s => s.Desc == "Inglés Técnico");


//                // 4) Courses (Cursos de Materias)
//                if (!context.Courses.Any())
//                {
//                    Console.WriteLine("Seed: Agregando cursos...");
//                    // Cursos para Programación I
//                    var c1 = new Course(0, 30, DateTime.Now.Year, "Tarde", "A") { SpecialtyId = specInf.Id };
//                    var c2 = new Course(0, 25, DateTime.Now.Year, "Mañana", "B") { SpecialtyId = specInf.Id };
//                    var c3 = new Course(0, 28, DateTime.Now.Year, "Noche", "C") { SpecialtyId = specInf.Id };

//                    // Cursos para Base de Datos I
//                    var c4 = new Course(0, 20, DateTime.Now.Year, "Tarde", "D") { SpecialtyId = specInf.Id };

//                    // Cursos para Economía General
//                    var c5 = new Course(0, 35, DateTime.Now.Year, "Mañana", "E") { SpecialtyId = specAdm.Id };

//                    // Curso de Contabilidad (año anterior)
//                    var c6 = new Course(0, 22, DateTime.Now.Year - 1, "Tarde", "F") { SpecialtyId = specCont.Id };

//                    context.Courses.AddRange(c1, c2, c3, c4, c5, c6);
//                    context.SaveChanges();
//                }

//                // Obtener cursos para usar en otros seeds
//                var courseProgATarde = context.Courses.First(c => c.Comision == "A" && c.Turno == "Tarde");
//                var courseProgBMñana = context.Courses.First(c => c.Comision == "B" && c.Turno == "Mañana");
//                var courseProgCNoche = context.Courses.First(c => c.Comision == "C" && c.Turno == "Noche");
//                var courseBDDTarde = context.Courses.First(c => c.Comision == "D" && c.Turno == "Tarde");
//                var courseEcoEMana = context.Courses.First(c => c.Comision == "E" && c.Turno == "Mañana");
//                var courseContFTarde = context.Courses.First(c => c.Comision == "F" && c.Turno == "Tarde");


//                // 5) CourseSubjects (relación Curso-Materia)
//                if (!context.CoursesSubjects.Any())
//                {
//                    Console.WriteLine("Seed: Agregando relaciones Curso-Materia...");
//                    // Programación I en distintos cursos
//                    var cs1_1 = new CourseSubject { CourseId = courseProgATarde.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Lun 18-20, Jue 18-20" };
//                    var cs1_2 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mar 09-11, Vie 09-11" };
//                    var cs1_3 = new CourseSubject { CourseId = courseProgCNoche.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mie 20-22, Sab 10-12" };

//                    // Base de Datos I en un curso
//                    var cs2_1 = new CourseSubject { CourseId = courseBDDTarde.Id, SubjectId = subjBD1.Id, DiaHoraDictado = "Mar 18-20" };

//                    // Estructuras de Datos en un curso (mismo curso de Programacion I B)
//                    var cs3_1 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjED.Id, DiaHoraDictado = "Lun 11-13, Mie 11-13" };

//                    // Economía General en un curso
//                    var cs4_1 = new CourseSubject { CourseId = courseEcoEMana.Id, SubjectId = subjEco.Id, DiaHoraDictado = "Lun 08-10, Vie 08-10" };

//                    // Contabilidad Financiera en un curso (año anterior)
//                    var cs5_1 = new CourseSubject { CourseId = courseContFTarde.Id, SubjectId = subjContFin.Id, DiaHoraDictado = "Jue 19-21" };

//                    context.CoursesSubjects.AddRange(cs1_1, cs1_2, cs1_3, cs2_1, cs3_1, cs4_1, cs5_1);
//                    context.SaveChanges();
//                }

//                // Obtener relaciones CourseSubject para usar en otros seeds
//                var csProgATarde = context.CoursesSubjects.First(cs => cs.CourseId == courseProgATarde.Id && cs.SubjectId == subjProgI.Id);
//                var csProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjProgI.Id);
//                var csProgCNoche = context.CoursesSubjects.First(cs => cs.CourseId == courseProgCNoche.Id && cs.SubjectId == subjProgI.Id);
//                var csBDDTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseBDDTarde.Id && cs.SubjectId == subjBD1.Id);
//                var csEDProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjED.Id);
//                var csEcoEMana = context.CoursesSubjects.First(cs => cs.CourseId == courseEcoEMana.Id && cs.SubjectId == subjEco.Id);
//                var csContFTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseContFTarde.Id && cs.SubjectId == subjContFin.Id);


//                // 6) Users (Administradores, Profesores, Estudiantes)
//                if (!context.Users.Any())
//                {
//                    Console.WriteLine("Seed: Agregando usuarios...");
//                    // Admin
//                    var adminPass = GeneratePasswordHash("admin123");
//                    var admin = new User(0, "admin", "Juan", "Admin", adminPass.hash, adminPass.salt, "admin@tpi.com", "42789654", "Av. San Martín 100", UserType.Admin, JobPositionType.Undefined, DateTime.Now, DateTime.Now, UserStatus.Active, null);
//                    // Nota: JobPositionType, DateOfAdmission, DateOfHire y StudentNumber se pasan en el orden del constructor de User.

//                    // Profesores
//                    var profPass = GeneratePasswordHash("profesor123");
//                    var prof1 = new User(0, "profesor1", "Carlos", "García", profPass.hash, profPass.salt, "carlos.garcia@tpi.com", "20123456", "Calle Ficticia 101", UserType.Teacher, JobPositionType.FullTime, DateTime.Now.AddYears(-5), DateTime.Now.AddYears(-5), UserStatus.Active, null);
//                    var prof2 = new User(0, "profesor2", "Laura", "Martínez", profPass.hash, profPass.salt, "laura.martinez@tpi.com", "22345678", "Av. Siempre Viva 742", UserType.Teacher, JobPositionType.PartTime, DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-2), UserStatus.Active, null);
//                    var prof3 = new User(0, "profesor3", "Roberto", "Flores", profPass.hash, profPass.salt, "roberto.flores@tpi.com", "25896321", "Bv. Oroño 500", UserType.Teacher, JobPositionType.Contract, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1), UserStatus.Active, null);


//                    // Estudiantes
//                    var studentPass = GeneratePasswordHash("estudiante123");
//                    var student1 = new User(0, "estudiante1", "Ana", "Gómez", studentPass.hash, studentPass.salt, "ana.gomez@example.com", "30123456", "C/ Falsa 123", UserType.Student, null, null, null, UserStatus.Active, "L-1001");
//                    var student2 = new User(0, "estudiante2", "Luis", "Pérez", studentPass.hash, studentPass.salt, "luis.perez@example.com", "40123456", "C/ Verdadera 456", UserType.Student, null, null, null, UserStatus.Active, "L-1002");
//                    var student3 = new User(0, "estudiante3", "Sofía", "Ramírez", studentPass.hash, studentPass.salt, "sofia.ramirez@example.com", "35456789", "Av. Central 890", UserType.Student, null, null, null, UserStatus.Active, "L-1003");
//                    var student4 = new User(0, "estudiante4", "Pablo", "Díaz", studentPass.hash, studentPass.salt, "pablo.diaz@example.com", "38765432", "Pje. del Sol 50", UserType.Student, null, null, null, UserStatus.Active, "L-1004");

//                    context.Users.AddRange(admin, prof1, prof2, prof3, student1, student2, student3, student4);
//                    context.SaveChanges();
//                }

//                // Obtener usuarios para usar en otros seeds
//                var userAdmin = context.Users.First(u => u.UserName == "admin");
//                var userProf1 = context.Users.First(u => u.UserName == "profesor1"); // Carlos García
//                var userProf2 = context.Users.First(u => u.UserName == "profesor2"); // Laura Martínez
//                var userProf3 = context.Users.First(u => u.UserName == "profesor3"); // Roberto Flores

//                var userStudent1 = context.Users.First(u => u.UserName == "estudiante1"); // Ana Gómez
//                var userStudent2 = context.Users.First(u => u.UserName == "estudiante2"); // Luis Pérez
//                var userStudent3 = context.Users.First(u => u.UserName == "estudiante3"); // Sofía Ramírez
//                var userStudent4 = context.Users.First(u => u.UserName == "estudiante4"); // Pablo Díaz


//                // 7) UserCourseSubject (Inscripciones de estudiantes y asignaciones de profesores)
//                if (!context.UsersCoursesSubjects.Any())
//                {
//                    Console.WriteLine("Seed: Agregando inscripciones y asignaciones...");

//                    // Asignar profesores a CourseSubjects (nota: los profesores no tienen NotaFinal)
//                    var ucsProf1_1 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
//                    var ucsProf1_2 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2) };
//                    var ucsProf2_1 = new UserCourseSubject { UserId = userProf2.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
//                    var ucsProf3_1 = new UserCourseSubject { UserId = userProf3.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1) };

//                    // Inscripciones de estudiantes (con notas)
//                    var ucsStudent1_1 = new UserCourseSubject { UserId = userStudent1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 8.5m };
//                    var ucsStudent1_2 = new UserCourseSubject { UserId = userStudent1.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = 7.0m };
//                    var ucsStudent2_1 = new UserCourseSubject { UserId = userStudent2.Id, CourseId = csProgBMñana.CourseId, SubjectId = csProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 9.0m };
//                    var ucsStudent2_2 = new UserCourseSubject { UserId = userStudent2.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = null }; // Pendiente
//                    var ucsStudent3_1 = new UserCourseSubject { UserId = userStudent3.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 6.0m };
//                    var ucsStudent4_1 = new UserCourseSubject { UserId = userStudent4.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = 7.5m };
//                    var ucsStudent4_2 = new UserCourseSubject { UserId = userStudent4.Id, CourseId = csContFTarde.CourseId, SubjectId = csContFTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-6), NotaFinal = 5.0m }; // Aprobado justo

//                    context.UsersCoursesSubjects.AddRange(
//                        ucsProf1_1, ucsProf1_2, ucsProf2_1, ucsProf3_1,
//                        ucsStudent1_1, ucsStudent1_2, ucsStudent2_1, ucsStudent2_2, ucsStudent3_1, ucsStudent4_1, ucsStudent4_2
//                    );
//                    context.SaveChanges();
//                }

//                tx.Commit();
//                Console.WriteLine("Seed: ¡Base de datos inicializada con éxito!");
//            }
//            catch (Exception ex)
//            {
//                tx.Rollback();
//                Console.WriteLine($"Seed ERROR: {ex.Message}");
//                throw;
//            }
//        }
//    }
//}

//using Domain.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Data
//{
//    public static class DbSeeder
//    {
//        public static void Initialize(TPIContext context)
//        {
//            if (context == null) throw new ArgumentNullException(nameof(context));

//            using var tx = context.Database.BeginTransaction();
//            try
//            {
//                // 1) Specialties
//                if (!context.Specialties.Any())
//                {
//                    Console.WriteLine("Seed: Agregando especialidades...");
//                    var s1 = new Specialty(0, "Informática", 5);
//                    var s2 = new Specialty(0, "Administración", 4);
//                    var s3 = new Specialty(0, "Contabilidad", 4);
//                    var s4 = new Specialty(0, "Recursos Humanos", 3);

//                    context.Specialties.AddRange(s1, s2, s3, s4);
//                    context.SaveChanges();
//                }

//                // Obtener especialidades para usar en otros seeds
//                var specInf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
//                var specAdm = context.Specialties.First(s => s.DescEspecialidad == "Administración");
//                var specCont = context.Specialties.First(s => s.DescEspecialidad == "Contabilidad");
//                var specRRHH = context.Specialties.First(s => s.DescEspecialidad == "Recursos Humanos");


//                // 2) Plans
//                if (!context.Plans.Any())
//                {
//                    Console.WriteLine("Seed: Agregando planes...");
//                    // Constructor: public Plan(int id, string descripcion, int año_calendario, int specialtyId, bool isDeleted = false)
//                    var p1 = new Plan(0, "Plan 2024 - Informática", 2024, specInf.Id);
//                    var p2 = new Plan(0, "Plan 2024 - Administración", 2024, specAdm.Id);
//                    var p3 = new Plan(0, "Plan 2023 - Contabilidad", 2023, specCont.Id);
//                    var p4 = new Plan(0, "Plan 2025 - Recursos Humanos", 2025, specRRHH.Id);
//                    var p5 = new Plan(0, "Plan 2020 - Informática (Obsoleto)", 2020, specInf.Id, isDeleted: true);


//                    context.Plans.AddRange(p1, p2, p3, p4, p5);
//                    context.SaveChanges();
//                }

//                // Obtener planes para usar en otros seeds
//                var planInf24 = context.Plans.First(p => p.Descripcion.Contains("Informática") && p.Año_calendario == 2024);
//                var planAdm24 = context.Plans.First(p => p.Descripcion.Contains("Administración") && p.Año_calendario == 2024);
//                var planCont23 = context.Plans.First(p => p.Descripcion.Contains("Contabilidad") && p.Año_calendario == 2023);
//                var planRRHH25 = context.Plans.First(p => p.Descripcion.Contains("Recursos Humanos") && p.Año_calendario == 2025);


//                // 3) Subjects
//                if (!context.Subjects.Any())
//                {
//                    Console.WriteLine("Seed: Agregando materias...");
//                    // Constructor: public Subject(int id, string desc, int hsSemanales, bool obligatoria, int año, int planId, bool isDeleted = false)
//                    var subj1 = new Subject(0, "Programación I", 6, true, 1, planInf24.Id);
//                    var subj2 = new Subject(0, "Base de Datos I", 4, true, 2, planInf24.Id);
//                    var subj3 = new Subject(0, "Estructuras de Datos", 5, true, 2, planInf24.Id);
//                    var subj4 = new Subject(0, "Redes de Computadoras", 4, true, 3, planInf24.Id);

//                    var subj5 = new Subject(0, "Economía General", 4, true, 1, planAdm24.Id);
//                    var subj6 = new Subject(0, "Marketing Estratégico", 3, true, 2, planAdm24.Id);
//                    var subj7 = new Subject(0, "Contabilidad Financiera", 5, true, 1, planCont23.Id);
//                    var subj8 = new Subject(0, "Derecho Laboral", 3, true, 2, planRRHH25.Id);
//                    var subj9 = new Subject(0, "Inglés Técnico", 2, false, 1, planInf24.Id);

//                    context.Subjects.AddRange(subj1, subj2, subj3, subj4, subj5, subj6, subj7, subj8, subj9);
//                    context.SaveChanges();
//                }

//                // Obtener materias para usar en otros seeds
//                var subjProgI = context.Subjects.First(s => s.Desc == "Programación I");
//                var subjBD1 = context.Subjects.First(s => s.Desc == "Base de Datos I");
//                var subjED = context.Subjects.First(s => s.Desc == "Estructuras de Datos");
//                var subjRedes = context.Subjects.First(s => s.Desc == "Redes de Computadoras");
//                var subjEco = context.Subjects.First(s => s.Desc == "Economía General");
//                var subjMark = context.Subjects.First(s => s.Desc == "Marketing Estratégico");
//                var subjContFin = context.Subjects.First(s => s.Desc == "Contabilidad Financiera");
//                var subjDerLab = context.Subjects.First(s => s.Desc == "Derecho Laboral");
//                var subjIngles = context.Subjects.First(s => s.Desc == "Inglés Técnico");


//                // 4) Courses (Cursos de Materias)
//                if (!context.Courses.Any())
//                {
//                    Console.WriteLine("Seed: Agregando cursos...");
//                    // Constructor: public Course(int id, int cupo, int año_calendario, string turno, string comision)
//                    // La propiedad SpecialtyId se establece directamente después de la creación

//                    // Cursos para Informática
//                    var c1 = new Course(0, 30, DateTime.Now.Year, "Tarde", "A") { SpecialtyId = specInf.Id };
//                    var c2 = new Course(0, 25, DateTime.Now.Year, "Mañana", "B") { SpecialtyId = specInf.Id };
//                    var c3 = new Course(0, 28, DateTime.Now.Year, "Noche", "C") { SpecialtyId = specInf.Id };
//                    var c4 = new Course(0, 20, DateTime.Now.Year, "Tarde", "D") { SpecialtyId = specInf.Id };

//                    // Cursos para Administración
//                    var c5 = new Course(0, 35, DateTime.Now.Year, "Mañana", "E") { SpecialtyId = specAdm.Id };

//                    // Curso de Contabilidad (año anterior)
//                    var c6 = new Course(0, 22, DateTime.Now.Year - 1, "Tarde", "F") { SpecialtyId = specCont.Id };

//                    context.Courses.AddRange(c1, c2, c3, c4, c5, c6);
//                    context.SaveChanges();
//                }

//                // Obtener cursos para usar en otros seeds
//                var courseProgATarde = context.Courses.First(c => c.Comision == "A" && c.Turno == "Tarde");
//                var courseProgBMñana = context.Courses.First(c => c.Comision == "B" && c.Turno == "Mañana");
//                var courseProgCNoche = context.Courses.First(c => c.Comision == "C" && c.Turno == "Noche");
//                var courseBDDTarde = context.Courses.First(c => c.Comision == "D" && c.Turno == "Tarde");
//                var courseEcoEMana = context.Courses.First(c => c.Comision == "E" && c.Turno == "Mañana");
//                var courseContFTarde = context.Courses.First(c => c.Comision == "F" && c.Turno == "Tarde");


//                // 5) CourseSubjects (relación Curso-Materia)
//                if (!context.CoursesSubjects.Any())
//                {
//                    Console.WriteLine("Seed: Agregando relaciones Curso-Materia...");
//                    // Programación I en distintos cursos
//                    var cs1_1 = new CourseSubject { CourseId = courseProgATarde.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Lun 18-20, Jue 18-20" };
//                    var cs1_2 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mar 09-11, Vie 09-11" };
//                    var cs1_3 = new CourseSubject { CourseId = courseProgCNoche.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mie 20-22, Sab 10-12" };

//                    // Base de Datos I en un curso
//                    var cs2_1 = new CourseSubject { CourseId = courseBDDTarde.Id, SubjectId = subjBD1.Id, DiaHoraDictado = "Mar 18-20" };

//                    // Estructuras de Datos en un curso (mismo curso de Programacion I B)
//                    var cs3_1 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjED.Id, DiaHoraDictado = "Lun 11-13, Mie 11-13" };

//                    // Economía General en un curso
//                    var cs4_1 = new CourseSubject { CourseId = courseEcoEMana.Id, SubjectId = subjEco.Id, DiaHoraDictado = "Lun 08-10, Vie 08-10" };

//                    // Contabilidad Financiera en un curso (año anterior)
//                    var cs5_1 = new CourseSubject { CourseId = courseContFTarde.Id, SubjectId = subjContFin.Id, DiaHoraDictado = "Jue 19-21" };

//                    context.CoursesSubjects.AddRange(cs1_1, cs1_2, cs1_3, cs2_1, cs3_1, cs4_1, cs5_1);
//                    context.SaveChanges();
//                }

//                // Obtener relaciones CourseSubject para usar en otros seeds
//                var csProgATarde = context.CoursesSubjects.First(cs => cs.CourseId == courseProgATarde.Id && cs.SubjectId == subjProgI.Id);
//                var csProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjProgI.Id);
//                var csProgCNoche = context.CoursesSubjects.First(cs => cs.CourseId == courseProgCNoche.Id && cs.SubjectId == subjProgI.Id);
//                var csBDDTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseBDDTarde.Id && cs.SubjectId == subjBD1.Id);
//                var csEDProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjED.Id);
//                var csEcoEMana = context.CoursesSubjects.First(cs => cs.CourseId == courseEcoEMana.Id && cs.SubjectId == subjEco.Id);
//                var csContFTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseContFTarde.Id && cs.SubjectId == subjContFin.Id);


//                // 6) Users (Administradores, Profesores, Estudiantes)
//                if (!context.Users.Any())
//                {
//                    Console.WriteLine("Seed: Agregando usuarios...");
//                    // Constructor: public User(int id, string userName, string password, string name, string lastname, string email, string adress, UserType typeUser, string dni, string? studentNumber = null, JobPositionType? jobPosition = null, DateTime? dateOfAdmission = null, DateTime? dateOfHire = null)

//                    // Admin (solo se usa el password en texto plano para el constructor)
//                    var admin = new User(0, "admin", "admin123", "Juan", "Admin", "admin@tpi.com", "Av. San Martín 100", UserType.Admin, "42789654");

//                    // Profesores
//                    var prof1 = new User(0, "profesor1", "profesor123", "Carlos", "García", "carlos.garcia@tpi.com", "Calle Ficticia 101", UserType.Teacher, "20123456",
//                                         jobPosition: JobPositionType.Practice, dateOfHire: DateTime.Now.AddYears(-5));
//                    var prof2 = new User(0, "profesor2", "profesor123", "Laura", "Martínez", "laura.martinez@tpi.com", "Av. Siempre Viva 742", UserType.Teacher, "22345678",
//                                         jobPosition: JobPositionType.Theory, dateOfHire: DateTime.Now.AddYears(-2));
//                    var prof3 = new User(0, "profesor3", "profesor123", "Roberto", "Flores", "roberto.flores@tpi.com", "Bv. Oroño 500", UserType.Teacher, "25896321",
//                                         jobPosition: JobPositionType.Practice, dateOfHire: DateTime.Now.AddYears(-1));


//                    // Estudiantes
//                    var student1 = new User(0, "estudiante1", "estudiante123", "Ana", "Gómez", "ana.gomez@example.com", "C/ Falsa 123", UserType.Student, "30123456",
//                                            studentNumber: "L-1001", dateOfAdmission: DateTime.Now.AddMonths(-2));
//                    var student2 = new User(0, "estudiante2", "estudiante123", "Luis", "Pérez", "luis.perez@example.com", "C/ Verdadera 456", UserType.Student, "40123456",
//                                            studentNumber: "L-1002", dateOfAdmission: DateTime.Now.AddMonths(-2));
//                    var student3 = new User(0, "estudiante3", "estudiante123", "Sofía", "Ramírez", "sofia.ramirez@example.com", "Av. Central 890", UserType.Student, "35456789",
//                                            studentNumber: "L-1003", dateOfAdmission: DateTime.Now.AddMonths(-1));
//                    var student4 = new User(0, "estudiante4", "estudiante123", "Pablo", "Díaz", "pablo.diaz@example.com", "Pje. del Sol 50", UserType.Student, "38765432",
//                                            studentNumber: "L-1004", dateOfAdmission: DateTime.Now.AddMonths(-1));

//                    context.Users.AddRange(admin, prof1, prof2, prof3, student1, student2, student3, student4);
//                    context.SaveChanges();
//                }

//                // Obtener usuarios para usar en otros seeds
//                var userAdmin = context.Users.First(u => u.UserName == "admin");
//                var userProf1 = context.Users.First(u => u.UserName == "profesor1"); // Carlos García
//                var userProf2 = context.Users.First(u => u.UserName == "profesor2"); // Laura Martínez
//                var userProf3 = context.Users.First(u => u.UserName == "profesor3"); // Roberto Flores

//                var userStudent1 = context.Users.First(u => u.UserName == "estudiante1"); // Ana Gómez
//                var userStudent2 = context.Users.First(u => u.UserName == "estudiante2"); // Luis Pérez
//                var userStudent3 = context.Users.First(u => u.UserName == "estudiante3"); // Sofía Ramírez
//                var userStudent4 = context.Users.First(u => u.UserName == "estudiante4"); // Pablo Díaz


//                // 7) UserCourseSubject (Inscripciones de estudiantes y asignaciones de profesores)
//                if (!context.UsersCoursesSubjects.Any())
//                {
//                    Console.WriteLine("Seed: Agregando inscripciones y asignaciones...");

//                    // Asignar profesores a CourseSubjects (sin NotaFinal)
//                    var ucsProf1_1 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
//                    var ucsProf1_2 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2) };
//                    var ucsProf2_1 = new UserCourseSubject { UserId = userProf2.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
//                    var ucsProf3_1 = new UserCourseSubject { UserId = userProf3.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1) };

//                    // Inscripciones de estudiantes (con notas)
//                    var ucsStudent1_1 = new UserCourseSubject { UserId = userStudent1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 8.5m };
//                    var ucsStudent1_2 = new UserCourseSubject { UserId = userStudent1.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = 7.0m };
//                    var ucsStudent2_1 = new UserCourseSubject { UserId = userStudent2.Id, CourseId = csProgBMñana.CourseId, SubjectId = csProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 9.0m };
//                    var ucsStudent2_2 = new UserCourseSubject { UserId = userStudent2.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = null }; // Pendiente
//                    var ucsStudent3_1 = new UserCourseSubject { UserId = userStudent3.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 6.0m };
//                    var ucsStudent4_1 = new UserCourseSubject { UserId = userStudent4.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = 7.5m };
//                    var ucsStudent4_2 = new UserCourseSubject { UserId = userStudent4.Id, CourseId = csContFTarde.CourseId, SubjectId = csContFTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-6), NotaFinal = 5.0m }; // Aprobado justo

//                    context.UsersCoursesSubjects.AddRange(
//                        ucsProf1_1, ucsProf1_2, ucsProf2_1, ucsProf3_1,
//                        ucsStudent1_1, ucsStudent1_2, ucsStudent2_1, ucsStudent2_2, ucsStudent3_1, ucsStudent4_1, ucsStudent4_2
//                    );
//                    context.SaveChanges();
//                }

//                tx.Commit();
//                Console.WriteLine("Seed: ¡Base de datos inicializada con éxito!");
//            }
//            catch (Exception ex)
//            {
//                tx.Rollback();
//                Console.WriteLine($"Seed ERROR: {ex.Message}");
//                throw;
//            }
//        }
//    }
//}

using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DbSeeder
    {
        //public static void Initialize(TPIContext context)
        //{
        //    if (context == null) throw new ArgumentNullException(nameof(context));

        //    using var tx = context.Database.BeginTransaction();
        //    try
        //    {
        //        // 1) Specialties
        //        if (!context.Specialties.Any())
        //        {
        //            var s1 = new Specialty(0, "Informática", 5);
        //            var s2 = new Specialty(0, "Administración", 4);
        //            context.Specialties.AddRange(s1, s2);
        //            context.SaveChanges();
        //        }

        //        // 2) Plans
        //        if (!context.Plans.Any())
        //        {
        //            var inf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
        //            var adm = context.Specialties.First(s => s.DescEspecialidad == "Administración");

        //            var p1 = new Plan(0, "Plan 2024 - Informática", 2024, inf.Id, false);
        //            var p2 = new Plan(0, "Plan 2024 - Administración", 2024, adm.Id, false);

        //            context.Plans.AddRange(p1, p2);
        //            context.SaveChanges();
        //        }

        //        // 3) Subjects
        //        if (!context.Subjects.Any())
        //        {
        //            var planInf = context.Plans.First(p => p.Descripcion.Contains("Informática"));
        //            var planAdm = context.Plans.First(p => p.Descripcion.Contains("Administración"));

        //            var subj1 = new Subject(0, "Programación I", 6, true, 1, planInf.Id, false);
        //            var subj2 = new Subject(0, "Base de Datos", 4, true, 2, planInf.Id, false);
        //            var subj3 = new Subject(0, "Economía", 4, true, 3, planAdm.Id, false);

        //            context.Subjects.AddRange(subj1, subj2, subj3);
        //            context.SaveChanges();
        //        }

        //        // 4) Courses
        //        //if (!context.Courses.Any())
        //        //
        //        //    var specinf = context.specialties.first(s => s.descespecialidad == "informática");
        //        //    var specadm = context.specialties.first(s => s.descespecialidad == "administración");

        //        //    var c1 = new course(0, 30, datetime.now.year, "tarde", "a") { specialtyid = specinf.id };
        //        //    var c2 = new course(0, 25, datetime.now.year, "mañana", "b") { specialtyid = specadm.id };

        //        //    context.courses.addrange(c1, c2);
        //        //    context.savechanges();
        //        //}

        //        // 5) CourseSubjects (relación Curso-Materia)
        //        //if (!context.CoursesSubjects.Any())
        //        //{
        //        //    var course1 = context.Courses.First();
        //        //    var course2 = context.Courses.Skip(1).FirstOrDefault();
        //        //    var subjProg = context.Subjects.First(s => s.Desc.Contains("Programación"));
        //        //    var subjBD = context.Subjects.First(s => s.Desc.Contains("Base de Datos"));

        //        //    var cs1 = new CourseSubject { CourseId = course1.Id, SubjectId = subjProg.Id, DiaHoraDictado = "Lun 18-20" };
        //        //    var cs2 = new CourseSubject { CourseId = course2.Id, SubjectId = subjBD.Id, DiaHoraDictado = "Mar 19-21" };

        //        //    context.CoursesSubjects.AddRange(cs1, cs2);
        //        //    context.SaveChanges();
        //        //}

        //        if (!context.Users.Any(u => u.TypeUser == UserType.Admin))
        //        {
        //            var admin = new User(0, "admin", "admin123", "Juan", "Admin", "admin@tpi.com", "Juan Jose Paso 123", Domain.Model.UserType.Admin, "42789654");
        //            context.Users.Add(admin);
        //            context.SaveChanges();
        //        }

        //        // 6) Usuarios (estudiantes de ejemplo)
        //        if (!context.Users.Any(u => u.TypeUser == UserType.Student))
        //        {
        //            var student1 = new User(0, "estudiante1", "Password123!", "Ana", "Gómez", "ana.gomez@example.com", "C/ Falsa 123", UserType.Student, "30123456", "L-1001");
        //            var student2 = new User(0, "estudiante2", "Password123!", "Luis", "Pérez", "luis.perez@example.com", "C/ Verdadera 456", UserType.Student, "40123456", "L-1002");

        //            context.Users.AddRange(student1, student2);
        //            context.SaveChanges();
        //        }

        //        // 7) Inscripciones / Notas
        //        if (!context.UsersCoursesSubjects.Any())
        //        {
        //            var alumno = context.Users.First(u => u.UserName == "estudiante1");
        //            var cs = context.CoursesSubjects.First();
        //            var enrollment = new UserCourseSubject
        //            {
        //                UserId = alumno.Id,
        //                CourseId = cs.CourseId,
        //                SubjectId = cs.SubjectId,
        //                FechaInscripcion = DateTime.UtcNow,
        //                NotaFinal = 8.5m
        //            };
        //            context.UsersCoursesSubjects.Add(enrollment);
        //            context.SaveChanges();
        //        }

        //        tx.Commit();
        //    }
        //    catch
        //    {
        //        tx.Rollback();
        //        throw;
        //    }
        //}
        public static void Initialize(TPIContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            using var tx = context.Database.BeginTransaction();
            try
            {
                // 1) Specialties
                if (!context.Specialties.Any())
                {
                    Console.WriteLine("Seed: Agregando especialidades...");
                    var s1 = new Specialty(0, "Informática", 5);
                    var s2 = new Specialty(0, "Administración", 4);
                    var s3 = new Specialty(0, "Contabilidad", 4);
                    var s4 = new Specialty(0, "Recursos Humanos", 3);

                    context.Specialties.AddRange(s1, s2, s3, s4);
                    context.SaveChanges();
                }

                // Obtener especialidades para usar en otros seeds
                var specInf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
                var specAdm = context.Specialties.First(s => s.DescEspecialidad == "Administración");
                var specCont = context.Specialties.First(s => s.DescEspecialidad == "Contabilidad");
                var specRRHH = context.Specialties.First(s => s.DescEspecialidad == "Recursos Humanos");


                // 2) Plans
                if (!context.Plans.Any())
                {
                    Console.WriteLine("Seed: Agregando planes...");
                    // Constructor: public Plan(int id, string descripcion, int año_calendario, int specialtyId, bool isDeleted = false)
                    var p1 = new Plan(0, "Plan 2024 - Informática", 2024, specInf.Id);
                    var p2 = new Plan(0, "Plan 2024 - Administración", 2024, specAdm.Id);
                    var p3 = new Plan(0, "Plan 2023 - Contabilidad", 2023, specCont.Id);
                    var p4 = new Plan(0, "Plan 2025 - Recursos Humanos", 2025, specRRHH.Id);
                    var p5 = new Plan(0, "Plan 2020 - Informática (Obsoleto)", 2020, specInf.Id, isDeleted: true);


                    context.Plans.AddRange(p1, p2, p3, p4, p5);
                    context.SaveChanges();
                }

                // Obtener planes para usar en otros seeds
                var planInf24 = context.Plans.First(p => p.Descripcion.Contains("Informática") && p.Año_calendario == 2024);
                var planAdm24 = context.Plans.First(p => p.Descripcion.Contains("Administración") && p.Año_calendario == 2024);
                var planCont23 = context.Plans.First(p => p.Descripcion.Contains("Contabilidad") && p.Año_calendario == 2023);
                var planRRHH25 = context.Plans.First(p => p.Descripcion.Contains("Recursos Humanos") && p.Año_calendario == 2025);


                // 3) Subjects
                if (!context.Subjects.Any())
                {
                    Console.WriteLine("Seed: Agregando materias...");
                    // Constructor: public Subject(int id, string desc, int hsSemanales, bool obligatoria, int año, int planId, bool isDeleted = false)
                    var subj1 = new Subject(0, "Programación I", 6, true, 1, planInf24.Id);
                    var subj2 = new Subject(0, "Base de Datos I", 4, true, 2, planInf24.Id);
                    var subj3 = new Subject(0, "Estructuras de Datos", 5, true, 2, planInf24.Id);
                    var subj4 = new Subject(0, "Redes de Computadoras", 4, true, 3, planInf24.Id);

                    var subj5 = new Subject(0, "Economía General", 4, true, 1, planAdm24.Id);
                    var subj6 = new Subject(0, "Marketing Estratégico", 3, true, 2, planAdm24.Id);
                    var subj7 = new Subject(0, "Contabilidad Financiera", 5, true, 1, planCont23.Id);
                    var subj8 = new Subject(0, "Derecho Laboral", 3, true, 2, planRRHH25.Id);
                    var subj9 = new Subject(0, "Inglés Técnico", 2, false, 1, planInf24.Id);

                    context.Subjects.AddRange(subj1, subj2, subj3, subj4, subj5, subj6, subj7, subj8, subj9);
                    context.SaveChanges();
                }

                // Obtener materias para usar en otros seeds
                var subjProgI = context.Subjects.First(s => s.Desc == "Programación I");
                var subjBD1 = context.Subjects.First(s => s.Desc == "Base de Datos I");
                var subjED = context.Subjects.First(s => s.Desc == "Estructuras de Datos");
                var subjRedes = context.Subjects.First(s => s.Desc == "Redes de Computadoras");
                var subjEco = context.Subjects.First(s => s.Desc == "Economía General");
                var subjMark = context.Subjects.First(s => s.Desc == "Marketing Estratégico");
                var subjContFin = context.Subjects.First(s => s.Desc == "Contabilidad Financiera");
                var subjDerLab = context.Subjects.First(s => s.Desc == "Derecho Laboral");
                var subjIngles = context.Subjects.First(s => s.Desc == "Inglés Técnico");


                // 4) Courses (Cursos de Materias)
                //if (!context.Courses.Any())
                //{
                //    Console.WriteLine("Seed: Agregando cursos...");
                //    // Constructor: public Course(int id, int cupo, int año_calendario, string turno, string comision, int specialtyId, bool isDeleted = false)
                //    // Puedes pasar SpecialtyId directamente al constructor si lo añades ahí, o seguir setéandolo como propiedad.

                //    // Cursos para Informática
                //    var c1 = new Course(0, 30, DateTime.Now.Year, "Tarde", "A", specInf.Id);
                //    var c2 = new Course(0, 25, DateTime.Now.Year, "Mañana", "B", specInf.Id);
                //    var c3 = new Course(0, 28, DateTime.Now.Year, "Noche", "C", specInf.Id);
                //    var c4 = new Course(0, 20, DateTime.Now.Year, "Tarde", "D", specInf.Id);

                //    // Cursos para Administración
                //    var c5 = new Course(0, 35, DateTime.Now.Year, "Mañana", "E", specAdm.Id);

                //    // Curso de Contabilidad (año anterior)
                //    var c6 = new Course(0, 22, DateTime.Now.Year - 1, "Tarde", "F", specCont.Id);

                //    context.Courses.AddRange(c1, c2, c3, c4, c5, c6);
                //    context.SaveChanges();
                //}
                if (!context.Courses.Any())
                {
                    Console.WriteLine("Seed: Agregando cursos...");
                    // Constructor: public Course(int id, int cupo, int año_calendario, string turno, string comision)
                    // La propiedad SpecialtyId se establece directamente después de la creación

                    // Cursos para Informática
                    var c1 = new Course(0, 30, DateTime.Now.Year, "Tarde", "A") { SpecialtyId = specInf.Id };
                    var c2 = new Course(0, 25, DateTime.Now.Year, "Mañana", "B") { SpecialtyId = specInf.Id };
                    var c3 = new Course(0, 28, DateTime.Now.Year, "Noche", "C") { SpecialtyId = specInf.Id };
                    var c4 = new Course(0, 20, DateTime.Now.Year, "Tarde", "D") { SpecialtyId = specInf.Id };

                    // Cursos para Administración
                    var c5 = new Course(0, 35, DateTime.Now.Year, "Mañana", "E") { SpecialtyId = specAdm.Id };

                    // Curso de Contabilidad (año anterior)
                    var c6 = new Course(0, 22, DateTime.Now.Year - 1, "Tarde", "F") { SpecialtyId = specCont.Id };

                    context.Courses.AddRange(c1, c2, c3, c4, c5, c6);
                    context.SaveChanges();
                }

                // Obtener cursos para usar en otros seeds
                var courseProgATarde = context.Courses.First(c => c.Comision == "A" && c.Turno == "Tarde");
                var courseProgBMñana = context.Courses.First(c => c.Comision == "B" && c.Turno == "Mañana");
                var courseProgCNoche = context.Courses.First(c => c.Comision == "C" && c.Turno == "Noche");
                var courseBDDTarde = context.Courses.First(c => c.Comision == "D" && c.Turno == "Tarde");
                var courseEcoEMana = context.Courses.First(c => c.Comision == "E" && c.Turno == "Mañana");
                var courseContFTarde = context.Courses.First(c => c.Comision == "F" && c.Turno == "Tarde");


                // 5) CourseSubjects (relación Curso-Materia)
                if (!context.CoursesSubjects.Any())
                {
                    Console.WriteLine("Seed: Agregando relaciones Curso-Materia...");
                    // Programación I en distintos cursos
                    var cs1_1 = new CourseSubject { CourseId = courseProgATarde.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Lun 18-20, Jue 18-20" };
                    var cs1_2 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mar 09-11, Vie 09-11" };
                    var cs1_3 = new CourseSubject { CourseId = courseProgCNoche.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mie 20-22, Sab 10-12" };

                    // Base de Datos I en un curso
                    var cs2_1 = new CourseSubject { CourseId = courseBDDTarde.Id, SubjectId = subjBD1.Id, DiaHoraDictado = "Mar 18-20" };

                    // Estructuras de Datos en un curso (mismo curso de Programacion I B)
                    var cs3_1 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjED.Id, DiaHoraDictado = "Lun 11-13, Mie 11-13" };

                    // Economía General en un curso
                    var cs4_1 = new CourseSubject { CourseId = courseEcoEMana.Id, SubjectId = subjEco.Id, DiaHoraDictado = "Lun 08-10, Vie 08-10" };

                    // Contabilidad Financiera en un curso (año anterior)
                    var cs5_1 = new CourseSubject { CourseId = courseContFTarde.Id, SubjectId = subjContFin.Id, DiaHoraDictado = "Jue 19-21" };

                    context.CoursesSubjects.AddRange(cs1_1, cs1_2, cs1_3, cs2_1, cs3_1, cs4_1, cs5_1);
                    context.SaveChanges();
                }

                // Obtener relaciones CourseSubject para usar en otros seeds
                var csProgATarde = context.CoursesSubjects.First(cs => cs.CourseId == courseProgATarde.Id && cs.SubjectId == subjProgI.Id);
                var csProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjProgI.Id);
                var csProgCNoche = context.CoursesSubjects.First(cs => cs.CourseId == courseProgCNoche.Id && cs.SubjectId == subjProgI.Id);
                var csBDDTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseBDDTarde.Id && cs.SubjectId == subjBD1.Id);
                var csEDProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjED.Id);
                var csEcoEMana = context.CoursesSubjects.First(cs => cs.CourseId == courseEcoEMana.Id && cs.SubjectId == subjEco.Id);
                var csContFTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseContFTarde.Id && cs.SubjectId == subjContFin.Id);


                // 6) Users (Administradores, Profesores, Estudiantes)
                if (!context.Users.Any())
                {
                    Console.WriteLine("Seed: Agregando usuarios...");
                    // Constructor: public User(int id, string userName, string password, string name, string lastname, string email, string adress, UserType typeUser, string dni, string? studentNumber = null, JobPositionType? jobPosition = null, DateTime? dateOfAdmission = null, DateTime? dateOfHire = null, bool isDeleted = false)

                    // Admin - AHORA CREADO AQUÍ EN EL SEEDER
                    var admin = new User(0, "admin", "admin123", "Juan", "Admin", "admin@tpi.com", "Av. San Martín 100", UserType.Admin, "42789654");

                    // Profesores
                    var prof1 = new User(0, "profesor1", "profesor123", "Carlos", "García", "carlos.garcia@tpi.com", "Calle Ficticia 101", UserType.Teacher, "20123456",
                                         jobPosition: JobPositionType.Practice, dateOfHire: DateTime.Now.AddYears(-5));
                    var prof2 = new User(0, "profesor2", "profesor123", "Laura", "Martínez", "laura.martinez@tpi.com", "Av. Siempre Viva 742", UserType.Teacher, "22345678",
                                         jobPosition: JobPositionType.Theory, dateOfHire: DateTime.Now.AddYears(-2));
                    var prof3 = new User(0, "profesor3", "profesor123", "Roberto", "Flores", "roberto.flores@tpi.com", "Bv. Oroño 500", UserType.Teacher, "25896321",
                                         jobPosition: JobPositionType.Practice, dateOfHire: DateTime.Now.AddYears(-1));


                    // Estudiantes
                    var student1 = new User(0, "estudiante1", "estudiante123", "Ana", "Gómez", "ana.gomez@example.com", "C/ Falsa 123", UserType.Student, "30123456",
                                             studentNumber: "L-1001", dateOfAdmission: DateTime.Now.AddMonths(-2));
                    var student2 = new User(0, "estudiante2", "estudiante123", "Luis", "Pérez", "luis.perez@example.com", "C/ Verdadera 456", UserType.Student, "40123456",
                                             studentNumber: "L-1002", dateOfAdmission: DateTime.Now.AddMonths(-2));
                    var student3 = new User(0, "estudiante3", "estudiante123", "Sofía", "Ramírez", "sofia.ramirez@example.com", "Av. Central 890", UserType.Student, "35456789",
                                             studentNumber: "L-1003", dateOfAdmission: DateTime.Now.AddMonths(-1));
                    var student4 = new User(0, "estudiante4", "estudiante123", "Pablo", "Díaz", "pablo.diaz@example.com", "Pje. del Sol 50", UserType.Student, "38765432",
                                             studentNumber: "L-1004", dateOfAdmission: DateTime.Now.AddMonths(-1));

                    context.Users.AddRange(admin, prof1, prof2, prof3, student1, student2, student3, student4);
                    context.SaveChanges();
                }

                // Obtener usuarios para usar en otros seeds
                var userAdmin = context.Users.First(u => u.UserName == "admin");
                var userProf1 = context.Users.First(u => u.UserName == "profesor1"); // Carlos García
                var userProf2 = context.Users.First(u => u.UserName == "profesor2"); // Laura Martínez
                var userProf3 = context.Users.First(u => u.UserName == "profesor3"); // Roberto Flores

                var userStudent1 = context.Users.First(u => u.UserName == "estudiante1"); // Ana Gómez
                var userStudent2 = context.Users.First(u => u.UserName == "estudiante2"); // Luis Pérez
                var userStudent3 = context.Users.First(u => u.UserName == "estudiante3"); // Sofía Ramírez
                var userStudent4 = context.Users.First(u => u.UserName == "estudiante4"); // Pablo Díaz


                // 7) UserCourseSubject (Inscripciones de estudiantes y asignaciones de profesores)
                if (!context.UsersCoursesSubjects.Any())
                {
                    Console.WriteLine("Seed: Agregando inscripciones y asignaciones...");

                    // Asignar profesores a CourseSubjects (sin NotaFinal)
                    var ucsProf1_1 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
                    var ucsProf1_2 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2) };
                    var ucsProf2_1 = new UserCourseSubject { UserId = userProf2.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
                    var ucsProf3_1 = new UserCourseSubject { UserId = userProf3.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1) };

                    // Inscripciones de estudiantes (con notas)
                    var ucsStudent1_1 = new UserCourseSubject { UserId = userStudent1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 8.5m };
                    var ucsStudent1_2 = new UserCourseSubject { UserId = userStudent1.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = 7.0m };
                    var ucsStudent2_1 = new UserCourseSubject { UserId = userStudent2.Id, CourseId = csProgBMñana.CourseId, SubjectId = csProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 9.0m };
                    var ucsStudent2_2 = new UserCourseSubject { UserId = userStudent2.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = null }; // Pendiente
                    var ucsStudent3_1 = new UserCourseSubject { UserId = userStudent3.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2), NotaFinal = 6.0m };
                    var ucsStudent4_1 = new UserCourseSubject { UserId = userStudent4.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1), NotaFinal = 7.5m };
                    var ucsStudent4_2 = new UserCourseSubject { UserId = userStudent4.Id, CourseId = csContFTarde.CourseId, SubjectId = csContFTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-6), NotaFinal = 5.0m }; // Aprobado justo

                    context.UsersCoursesSubjects.AddRange(
                        ucsProf1_1, ucsProf1_2, ucsProf2_1, ucsProf3_1,
                        ucsStudent1_1, ucsStudent1_2, ucsStudent2_1, ucsStudent2_2, ucsStudent3_1, ucsStudent4_1, ucsStudent4_2
                    );
                    context.SaveChanges();
                }

                tx.Commit();
                Console.WriteLine("Seed: ¡Base de datos inicializada con éxito!");
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine($"Seed ERROR: {ex.Message}");
                throw;
            }
        }
    }
}
