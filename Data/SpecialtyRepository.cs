using System.Numerics;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SpecialtyRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Specialty specialty)
        {
            using var context = CreateContext();
            await context.Specialties.AddAsync(specialty);
            await context.SaveChangesAsync();
        }

        public async Task <Specialty?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Specialties
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Specialties
                .ToListAsync();
        }
        public async Task<bool> UpdateAsync(Specialty specialty)
        {
            using var context = CreateContext();
            var existing = await context.Specialties.FindAsync(specialty.Id);
            if (existing != null)
            {
                existing.SetDescEspecialidad(specialty.DescEspecialidad);
                existing.SetDuracionAnios(specialty.DuracionAnios);
                existing.IsDeleted = specialty.IsDeleted;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

     

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var specialty = await context.Specialties.FindAsync(id);
            if (specialty != null && !specialty.IsDeleted)
            {
                specialty.IsDeleted = false;
                context.Specialties.Update(specialty);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }
}

