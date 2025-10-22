using Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class PlanRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        } 
        public void Add (Plan plan)
        {
            using var context = CreateContext();
            context.Plans.Add(plan);
            context.SaveChanges();
        }
        public Plan? Get(int id)
        {
            using var context = CreateContext();
            return context.Plans.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }

        public IEnumerable<Plan> GetAll()
        {
            using var context = CreateContext();
            return context.Plans
                .Where(p => !p.IsDeleted)
                .ToList(); 
        }

        public bool Update(Plan plan)
        {
            using var context = CreateContext();
            var existingPlan = context.Plans.Find(plan.Id);
            if (existingPlan != null && !existingPlan.IsDeleted)
            {
                existingPlan.SetAño_calendario(plan.Año_calendario);
                existingPlan.SetDescripcion(plan.Descripcion);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id) 
        { 
            using var context = CreateContext();
            var plan = context.Plans.Find(id);
            if (plan != null && !plan.IsDeleted) 
            {
                plan.IsDeleted = true; 
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Subject> GetSubjects(int planId)
        {
            using var ctx = CreateContext();
            return ctx.Subjects
                .Where(s => s.PlanId == planId && s.Habilitado)
                .ToList();
        }

    }
}