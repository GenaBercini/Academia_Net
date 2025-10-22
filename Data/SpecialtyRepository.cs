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

        public void Add(Specialty specialty)
        {
            using var context = CreateContext();
            context.Specialties.Add(specialty);
            context.SaveChanges();
        }

        public Specialty? Get(int id)
        {
            using var context = CreateContext();
            return context.Specialties
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Specialty> GetAll()
        {
            using var context = CreateContext();
            return context.Specialties
                .ToList();
        }

        public bool Update(Specialty specialty)
        {
            using var context = CreateContext();
            var existing = context.Specialties.FirstOrDefault(s => s.Id == specialty.Id);

            if (existing != null)
            {
                existing.SetDescEspecialidad(specialty.DescEspecialidad);
                existing.SetDuracionAnios(specialty.DuracionAnios);
                existing.Habilitado = specialty.Habilitado;

                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var specialty = context.Specialties.Find(id);
            if (specialty != null)
            {
                specialty.Habilitado = false; 
                context.SaveChanges();
                return true;
            }
            return false;
        }

        //Cada especialidad tiene que buscar los planes a los que esta asociados
        public IEnumerable<Plan> GetPlans(int specialtyId)
        {
            using var ctx = CreateContext();
            return ctx.Plans
                .Where(p => p.SpecialtyId == specialtyId && !p.IsDeleted)
                .ToList();
        }

    }
}

