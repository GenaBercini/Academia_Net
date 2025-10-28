using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Types;

namespace Data
{
    public static class DbSeeder
    {
        public static void Initialize(TPIContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            using var tx = context.Database.BeginTransaction();
            try
            {
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

                var specInf = context.Specialties.First(s => s.DescEspecialidad == "Informática");
                var specAdm = context.Specialties.First(s => s.DescEspecialidad == "Administración");
                var specCont = context.Specialties.First(s => s.DescEspecialidad == "Contabilidad");
                var specRRHH = context.Specialties.First(s => s.DescEspecialidad == "Recursos Humanos");


                if (!context.Plans.Any())
                {
                    Console.WriteLine("Seed: Agregando planes...");
                    var p1 = new Plan(0, "Plan 2024 - Informática", 2024, specInf.Id);
                    var p2 = new Plan(0, "Plan 2024 - Administración", 2024, specAdm.Id);
                    var p3 = new Plan(0, "Plan 2023 - Contabilidad", 2023, specCont.Id);
                    var p4 = new Plan(0, "Plan 2025 - Recursos Humanos", 2025, specRRHH.Id);
                    var p5 = new Plan(0, "Plan 2020 - Informática (Obsoleto)", 2020, specInf.Id, isDeleted: true);


                    context.Plans.AddRange(p1, p2, p3, p4, p5);
                    context.SaveChanges();
                }

                var planInf24 = context.Plans.First(p => p.Descripcion.Contains("Informática") && p.Año_calendario == 2024);
                var planAdm24 = context.Plans.First(p => p.Descripcion.Contains("Administración") && p.Año_calendario == 2024);
                var planCont23 = context.Plans.First(p => p.Descripcion.Contains("Contabilidad") && p.Año_calendario == 2023);
                var planRRHH25 = context.Plans.First(p => p.Descripcion.Contains("Recursos Humanos") && p.Año_calendario == 2025);


                if (!context.Subjects.Any())
                {
                    Console.WriteLine("Seed: Agregando materias...");
                   
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

                var subjProgI = context.Subjects.First(s => s.Desc == "Programación I");
                var subjBD1 = context.Subjects.First(s => s.Desc == "Base de Datos I");
                var subjED = context.Subjects.First(s => s.Desc == "Estructuras de Datos");
                var subjRedes = context.Subjects.First(s => s.Desc == "Redes de Computadoras");
                var subjEco = context.Subjects.First(s => s.Desc == "Economía General");
                var subjMark = context.Subjects.First(s => s.Desc == "Marketing Estratégico");
                var subjContFin = context.Subjects.First(s => s.Desc == "Contabilidad Financiera");
                var subjDerLab = context.Subjects.First(s => s.Desc == "Derecho Laboral");
                var subjIngles = context.Subjects.First(s => s.Desc == "Inglés Técnico");

                if (!context.Courses.Any())
                {
                    Console.WriteLine("Seed: Agregando cursos...");
                    var c1 = new Course(0, 30, DateTime.Now.Year, "Tarde", "101", specInf.Id);
                    var c2 = new Course(0, 25, DateTime.Now.Year, "Mañana", "201", specInf.Id);
                    var c3 = new Course(0, 28, DateTime.Now.Year, "Noche", "303", specInf.Id);
                    var c4 = new Course (0, 20, DateTime.Now.Year, "Tarde", "404", specInf.Id);

                    var c5 = new Course(0, 35, DateTime.Now.Year, "Mañana", "505",specAdm.Id);

                    var c6 = new Course(0, 22, DateTime.Now.Year - 1, "Tarde", "504", specCont.Id);

                    context.Courses.AddRange(c1, c2, c3, c4, c5, c6);
                    context.SaveChanges();
                }

                var courseProgATarde = context.Courses.First(c => c.Comision == "101" && c.Turno == "Tarde");
                var courseProgBMñana = context.Courses.First(c => c.Comision == "201" && c.Turno == "Mañana");
                var courseProgCNoche = context.Courses.First(c => c.Comision == "303" && c.Turno == "Noche");
                var courseBDDTarde = context.Courses.First(c => c.Comision == "404" && c.Turno == "Tarde");
                var courseEcoEMana = context.Courses.First(c => c.Comision == "505" && c.Turno == "Mañana");
                var courseContFTarde = context.Courses.First(c => c.Comision == "504" && c.Turno == "Tarde");


                if (!context.CoursesSubjects.Any())
                {
                    Console.WriteLine("Seed: Agregando relaciones Curso-Materia...");
                    var cs1_1 = new CourseSubject { CourseId = courseProgATarde.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Lun 18-20, Jue 18-20" };
                    var cs1_2 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mar 09-11, Vie 09-11" };
                    var cs1_3 = new CourseSubject { CourseId = courseProgCNoche.Id, SubjectId = subjProgI.Id, DiaHoraDictado = "Mie 20-22, Sab 10-12" };

                    var cs2_1 = new CourseSubject { CourseId = courseBDDTarde.Id, SubjectId = subjBD1.Id, DiaHoraDictado = "Mar 18-20" };

                    var cs3_1 = new CourseSubject { CourseId = courseProgBMñana.Id, SubjectId = subjED.Id, DiaHoraDictado = "Lun 11-13, Mie 11-13" };

                    var cs4_1 = new CourseSubject { CourseId = courseEcoEMana.Id, SubjectId = subjEco.Id, DiaHoraDictado = "Lun 08-10, Vie 08-10" };

                    var cs5_1 = new CourseSubject { CourseId = courseContFTarde.Id, SubjectId = subjContFin.Id, DiaHoraDictado = "Jue 19-21" };

                    context.CoursesSubjects.AddRange(cs1_1, cs1_2, cs1_3, cs2_1, cs3_1, cs4_1, cs5_1);
                    context.SaveChanges();
                }

                var csProgATarde = context.CoursesSubjects.First(cs => cs.CourseId == courseProgATarde.Id && cs.SubjectId == subjProgI.Id);
                var csProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjProgI.Id);
                var csProgCNoche = context.CoursesSubjects.First(cs => cs.CourseId == courseProgCNoche.Id && cs.SubjectId == subjProgI.Id);
                var csBDDTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseBDDTarde.Id && cs.SubjectId == subjBD1.Id);
                var csEDProgBMñana = context.CoursesSubjects.First(cs => cs.CourseId == courseProgBMñana.Id && cs.SubjectId == subjED.Id);
                var csEcoEMana = context.CoursesSubjects.First(cs => cs.CourseId == courseEcoEMana.Id && cs.SubjectId == subjEco.Id);
                var csContFTarde = context.CoursesSubjects.First(cs => cs.CourseId == courseContFTarde.Id && cs.SubjectId == subjContFin.Id);


                if (!context.Users.Any())
                {
                    Console.WriteLine("Seed: Agregando usuarios...");
                    var admin = new User(0, "admin", "admin123", "Juan", "Admin", "admin@tpi.com", "Av. San Martín 100", UserType.Admin, "42789654");

                    var prof1 = new User(0, "profesor1", "profesor123", "Carlos", "García", "carlos.garcia@tpi.com", "Calle Ficticia 101", UserType.Teacher, "20123456",
                                         jobPosition: JobPositionType.Practice, dateOfHire: DateTime.Now.AddYears(-5));
                    var prof2 = new User(0, "profesor2", "profesor123", "Laura", "Martínez", "laura.martinez@tpi.com", "Av. Siempre Viva 742", UserType.Teacher, "22345678",
                                         jobPosition: JobPositionType.Theory, dateOfHire: DateTime.Now.AddYears(-2));
                    var prof3 = new User(0, "profesor3", "profesor123", "Roberto", "Flores", "roberto.flores@tpi.com", "Bv. Oroño 500", UserType.Teacher, "25896321",
                                         jobPosition: JobPositionType.Practice, dateOfHire: DateTime.Now.AddYears(-1));


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

                var userAdmin = context.Users.First(u => u.UserName == "admin");
                var userProf1 = context.Users.First(u => u.UserName == "profesor1"); 
                var userProf2 = context.Users.First(u => u.UserName == "profesor2"); 
                var userProf3 = context.Users.First(u => u.UserName == "profesor3"); 

                var userStudent1 = context.Users.First(u => u.UserName == "estudiante1"); 
                var userStudent2 = context.Users.First(u => u.UserName == "estudiante2");
                var userStudent3 = context.Users.First(u => u.UserName == "estudiante3"); 
                var userStudent4 = context.Users.First(u => u.UserName == "estudiante4"); 


                if (!context.UsersCoursesSubjects.Any())
                {
                    Console.WriteLine("Seed: Agregando inscripciones y asignaciones...");

                    var ucsProf1_1 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csProgATarde.CourseId, SubjectId = csProgATarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
                    var ucsProf1_2 = new UserCourseSubject { UserId = userProf1.Id, CourseId = csEDProgBMñana.CourseId, SubjectId = csEDProgBMñana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-2) };
                    var ucsProf2_1 = new UserCourseSubject { UserId = userProf2.Id, CourseId = csBDDTarde.CourseId, SubjectId = csBDDTarde.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-3) };
                    var ucsProf3_1 = new UserCourseSubject { UserId = userProf3.Id, CourseId = csEcoEMana.CourseId, SubjectId = csEcoEMana.SubjectId, FechaInscripcion = DateTime.UtcNow.AddMonths(-1) };

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
