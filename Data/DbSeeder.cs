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
    }
}
