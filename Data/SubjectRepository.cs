using System.Numerics;
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

        public async Task AddAsync(Subject subject)
        {
            using var context = CreateContext();
            bool planExists = context.Plans.Any(s => s.Id == subject.PlanId);
            if (!planExists)
                throw new InvalidOperationException($"No existe el plan con Id {subject.PlanId}");
            await context.Subjects.AddAsync(subject);
            await context.SaveChangesAsync();
        }
 
        public async Task<Subject?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Subjects
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Subjects
                .Where(p => !p.IsDeleted)
                .Include(p => p.Plan)
                .ToListAsync();
        }
 

        public async Task<bool> UpdateAsync(Subject subject)
        {
            using var context = CreateContext();
            var existingSubject = await context.Subjects
                .FirstOrDefaultAsync(s => s.Id == subject.Id);

            if (existingSubject != null && !existingSubject.IsDeleted)
            {
                existingSubject.SetDesc(subject.Desc);
                existingSubject.SetHsSemanales(subject.HsSemanales);
                existingSubject.SetObligatoria(subject.Obligatoria);
                existingSubject.SetPlanId(subject.PlanId);
                existingSubject.IsDeleted = subject.IsDeleted;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var subject = await context.Subjects.FindAsync(id);
            if (subject != null && !subject.IsDeleted)
            {
                subject.IsDeleted = false;
                context.Subjects.Update(subject); 
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}

