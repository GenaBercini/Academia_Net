using System.Numerics;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SubjectRepository
    {

        private readonly TPIContext _context;

        public SubjectRepository(TPIContext context)
        {
            _context = context;
        }
        //private TPIContext CreateContext()
        //{
        //    return new TPIContext();
        //}

        public async Task AddAsync(Subject subject)
        {
            //using var context = CreateContext();
            bool planExists = _context.Plans.Any(s => s.Id == subject.PlanId);
            if (!planExists)
                throw new InvalidOperationException($"No existe el plan con Id {subject.PlanId}");
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }
 
        public async Task<Subject?> GetAsync(int id)
        {
            //using var context = CreateContext();
            return await _context.Subjects
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            //using var context = CreateContext();
            return await _context.Subjects
                .Where(p => !p.IsDeleted)
                .Include(p => p.Plan)
                .ToListAsync();
        }
 

        public async Task<bool> UpdateAsync(Subject subject)
        {
            //using var context = CreateContext();
            var existingSubject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.Id == subject.Id);

            if (existingSubject != null && !existingSubject.IsDeleted)
            {
                existingSubject.SetDesc(subject.Desc);
                existingSubject.SetHsSemanales(subject.HsSemanales);
                existingSubject.SetObligatoria(subject.Obligatoria);
                existingSubject.SetPlanId(subject.PlanId);
                existingSubject.IsDeleted = subject.IsDeleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //using var context = CreateContext();
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null && !subject.IsDeleted)
            {
                subject.IsDeleted = false;
                _context.Subjects.Update(subject); 
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}

