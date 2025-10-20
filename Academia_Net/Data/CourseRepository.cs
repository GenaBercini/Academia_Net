using System;
using Domain.Model;


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
                .Where(c => !c.IsDeleted) 
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

        public bool Exists(int año_calendario, int comision, int excludeId)
        {
            using var contex = CreateContext();
            return contex.Courses.Any(c =>
                c.Id != excludeId
                && c.Año_calendario == año_calendario
                && c.Comision == comision
                && !c.IsDeleted);
        }
        public bool Exists(int año_calendario, int comision)
        {
            using var contex = CreateContext();
            return contex.Courses.Any(c =>
                c.Año_calendario == año_calendario
                && c.Comision == comision
                && !c.IsDeleted);
        }
    }
}
