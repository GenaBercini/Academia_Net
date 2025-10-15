using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SubjectRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public void Add(Subject subject)
        {
            using var context = CreateContext();
            context.Subjects.Add(subject);
            context.SaveChanges();
        }

        public Subject? Get(int id)
        {
            using var context = CreateContext();
            return context.Subjects
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Subject> GetAll()
        {
            using var context = CreateContext();
            return context.Subjects
                .ToList();
        }

        public bool Update(Subject subject)
        {
            using var context = CreateContext();
            var existing = context.Subjects.FirstOrDefault(s => s.Id == subject.Id);

            if (existing != null)
            {
                existing.SetDesc(subject.Desc);
                existing.SetHsSemanales(subject.HsSemanales);
                existing.SetObligatoria(subject.Obligatoria);
                existing.Habilitado = subject.Habilitado;

                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var subject = context.Subjects.Find(id);
            if (subject != null)
            {
                subject.Habilitado = false; 
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

