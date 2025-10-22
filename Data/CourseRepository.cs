using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;


namespace Data
{
    public class CourseRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public void Add(Course curso)
        {
            using var context = CreateContext();
            context.Courses.Add(curso);
            context.SaveChanges();
        }

        public Course? Get(int id)
        {
            using var context = CreateContext();
            return context.Courses
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted); 
        }

        public IEnumerable<Course> GetAll()
        {
            using var context = CreateContext();
            return context.Courses
                .Include(c => c.CoursesSubjects)
                .ThenInclude(cs => cs.Subject)
                .ToList();
        }

        public bool Update(Course curso)
        {
            using var context = CreateContext();
            var existingCurso = context.Courses.Find(curso.Id);
            if (existingCurso != null && !existingCurso.IsDeleted)
            {
                existingCurso.SetCupo(curso.Cupo);
                existingCurso.SetAño_calendario(curso.Año_calendario);
                existingCurso.SetTurno(curso.Turno);
                existingCurso.SetComision(curso.Comision);

                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var curso = context.Courses.Find(id);
            if (curso != null && !curso.IsDeleted)
            {
                curso.IsDeleted = true;  
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<CourseSubject> GetCourseSubjects(int courseId)
        {
            using var ctx = CreateContext();
            return ctx.CoursesSubjects
                      .Include(cs => cs.Subject)
                      .Where(cs => cs.CourseId == courseId)
                      .ToList();
        }

        public CourseSubject AddCourseSubject(int courseId, int subjectId, string? diaHora)
        {
            using var ctx = CreateContext();

            var course = ctx.Courses.FirstOrDefault(c => c.Id == courseId && !c.IsDeleted);
            if (course == null) throw new InvalidOperationException("Curso no encontrado.");

            var subject = ctx.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null) throw new InvalidOperationException("Materia no encontrada.");

            var existing = ctx.CoursesSubjects.Find(courseId, subjectId);
            if (existing != null) throw new InvalidOperationException("La materia ya está vinculada al curso.");

            var cs = new CourseSubject
            {
                CourseId = courseId,
                SubjectId = subjectId,
                DiaHoraDictado = diaHora
            };

            ctx.CoursesSubjects.Add(cs);
            ctx.SaveChanges();

            return ctx.CoursesSubjects
                      .Include(x => x.Subject)
                      .Include(x => x.Course)
                      .First(x => x.CourseId == courseId && x.SubjectId == subjectId);
        }

        public bool Exists(int año_calendario, string comision, int excludeId)
        {
            using var contex = CreateContext();
            return contex.Courses.Any(c =>
                c.Id != excludeId
                && c.Año_calendario == año_calendario
                && c.Comision == comision
                && !c.IsDeleted);
        }
        public bool Exists(int año_calendario, string comision)
        {
            using var contex = CreateContext();
            return contex.Courses.Any(c =>
                c.Año_calendario == año_calendario
                && c.Comision == comision
                && !c.IsDeleted);
        }
    }
}
