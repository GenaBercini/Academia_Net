using System.Numerics;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SpecialtyRepository
    {

        private readonly TPIContext _context;

        public SpecialtyRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Specialty specialty)
        {
            //using var context = CreateContext();
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task <Specialty?> GetAsync(int id)
        {
            //using var context = CreateContext();
            return await _context.Specialties
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            //using var context = CreateContext();
            return await _context.Specialties
                .ToListAsync();
        }
        public async Task<bool> UpdateAsync(Specialty specialty)
        {
            //using var context = CreateContext();
            var existing = await _context.Specialties.FindAsync(specialty.Id);
            if (existing != null)
            {
                existing.SetDescEspecialidad(specialty.DescEspecialidad);
                existing.SetDuracionAnios(specialty.DuracionAnios);
                existing.IsDeleted = specialty.IsDeleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

     

        public async Task<bool> DeleteAsync(int id)
        {
            //using var context = CreateContext();
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty != null && !specialty.IsDeleted)
            {
                specialty.IsDeleted = true;
                _context.Specialties.Update(specialty);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }
}

