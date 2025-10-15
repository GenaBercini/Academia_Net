using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PlanService
    {
        public PlanDTO Add(PlanDTO dto)
        {
            var planRepository = new PlanRepository();

            var plan = new Plan(
                0,
                dto.Descripcion,
                dto.Año_calendario
            );

            planRepository.Add(plan);

            dto.Id = plan.Id;
            return dto;
        }

        public bool Delete(int id)
        {
            var planRepository = new PlanRepository();
            return planRepository.Delete(id);
        }

        public PlanDTO? Get(int id)
        {
            var planRepository = new PlanRepository();
            Plan? plan = planRepository.Get(id);

            if (plan == null)
                return null;

            return new PlanDTO
            {
                Id = plan.Id,
                Año_calendario = plan.Año_calendario,
                Descripcion = plan.Descripcion
            };
        }

        public IEnumerable<PlanDTO> GetAll()
        {
            var planRepository = new PlanRepository();
            var planes = planRepository.GetAll();

            return planes.Select(plan => new PlanDTO
            {
                Id = plan.Id,
                Año_calendario = plan.Año_calendario,
                Descripcion = plan.Descripcion
            }).ToList();
        }

        public bool Update(PlanDTO dto)
        {
            var planRepository = new PlanRepository();

            var plan = new Plan(
                dto.Id,
                dto.Descripcion,
                dto.Año_calendario
            );

            return planRepository.Update(plan);
        }
    }
}
