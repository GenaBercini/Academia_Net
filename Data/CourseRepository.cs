using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;


namespace Data
{
    public class CourseRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Course course)
        {
            using var context = CreateContext();
            bool specialtyExists = context.Specialties.Any(s => s.Id == course.SpecialtyId);
            if (!specialtyExists)
                throw new InvalidOperationException($"No existe la especialidad con Id {course.SpecialtyId}");
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
        }

        public async Task<Course?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Courses
                .Include(c => c.Specialty)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted); 
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Courses
                .Where(p => !p.IsDeleted)
                .Include(p => p.Specialty)
                .ToListAsync();
        }
 
        public async Task<bool> UpdateAsync(Course course)
        {
            using var context = CreateContext();
            var existingCourse = await context.Courses
                .FirstOrDefaultAsync(c => c.Id == course.Id);
            if (existingCourse != null && !existingCourse.IsDeleted)
            {
                existingCourse.SetCupo(course.Cupo);
                existingCourse.SetAño_calendario(course.Año_calendario);
                existingCourse.SetTurno(course.Turno);
                existingCourse.SetComision(course.Comision);
                existingCourse.SetSpecialtyId(course.SpecialtyId);
                existingCourse.IsDeleted = course.IsDeleted;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var course = await context.Courses
                .FindAsync(id);
            if (course != null && !course.IsDeleted)
            {
                course.IsDeleted = true;
                context.Courses.Update(course);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Cada curso va a tener que buscar sus estudiantes y profesores asociados
        public async Task<IEnumerable<User>> GetStudents(int courseId)
        {
            using var ctx = CreateContext();
            return ctx.UsersCoursesSubjects
                .Include(ucs => ucs.User)
                .Where(ucs => ucs.CourseId == courseId)
                .Select(ucs => ucs.User)
                .Distinct()
                .ToList();
        }
        public async Task<IEnumerable<User>> GetTeachers(int courseId)
        {
            using var ctx = CreateContext();
            return ctx.UsersCoursesSubjects
                .Include(ucs => ucs.User)
                .Where(ucs => ucs.CourseId == courseId && ucs.User.TypeUser == UserType.Teacher)
                .Select(ucs => ucs.User)
                .Distinct()
                .ToList();
        }

        public async Task<IEnumerable<CourseSubject?>> GetCourseSubjects(int courseId)
        {
            using var ctx = CreateContext();
            return ctx.CoursesSubjects
                      .Include(cs => cs.Subject)
                      .Where(cs => cs.CourseId == courseId)
                      .ToList();
        }

        public async Task<CourseSubject> AddCourseSubject(int courseId, int subjectId, string? diaHora)
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

            await ctx.CoursesSubjects.AddAsync(cs);
            await ctx.SaveChangesAsync();

            return ctx.CoursesSubjects
                      .Include(x => x.Subject)
                      .Include(x => x.Course)
                      .First(x => x.CourseId == courseId && x.SubjectId == subjectId);
        }
    }
}
