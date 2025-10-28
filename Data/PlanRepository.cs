using System.Numerics;
using Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class PlanRepository
    {

        private readonly TPIContext _context;

        public PlanRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Plan plan)
        {
            //using var context = CreateContext();
            bool specialtyExists = _context.Specialties.Any(s => s.Id == plan.SpecialtyId);
            if (!specialtyExists)
                throw new InvalidOperationException($"No existe la especialidad con Id {plan.SpecialtyId}");
            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();
        }


        public async Task<Plan?> GetAsync(int id)
        {
            //using var context = CreateContext();
            return await _context.Plans
                .Include(p => p.Specialty)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            //using var context = CreateContext();
            return await _context.Plans
                .Where(p => !p.IsDeleted)
                .Include(p => p.Specialty)
                .ToListAsync();
        }


        public async Task<bool> UpdateAsync(Plan plan)
        {
            //using var context = CreateContext();
            var existingPlan = await _context.Plans
                .FirstOrDefaultAsync(p => p.Id == plan.Id  );

            if (existingPlan != null && !existingPlan.IsDeleted)
            {
                existingPlan.SetAño_calendario(plan.Año_calendario);
                existingPlan.SetDescripcion(plan.Descripcion);
                existingPlan.SetSpecialtyId(plan.SpecialtyId);
                existingPlan.IsDeleted = plan.IsDeleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //using var context = CreateContext();
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null && !plan.IsDeleted)
            {
                plan.IsDeleted = true;
                _context.Plans.Update(plan);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}