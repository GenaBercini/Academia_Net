using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PlanService
    {
        private void ValidarPlanDTO(PlanDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ArgumentException("La descripción es obligatoria.");

            if (dto.Descripcion.Length < 3 || dto.Descripcion.Length > 100)
                throw new ArgumentException("La descripción debe tener entre 3 y 100 caracteres.");

            int añoActual = DateTime.Now.Year;
            if (dto.Año_calendario < 2000 || dto.Año_calendario > añoActual + 1)
                throw new ArgumentException("Año calendario inválido.");
        }

        public PlanDTO Add(PlanDTO dto)
        {
            ValidarPlanDTO(dto);
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
            if (dto == null)
                throw new ArgumentException("Los datos del plan no pueden ser nulos.");

            ValidarPlanDTO(dto);

            var planRepository = new PlanRepository();

            var existingPlan = planRepository.Get(dto.Id);
            if (existingPlan == null)
                throw new ArgumentException("El plan no existe.");

            var plan = new Plan(
                dto.Id,
                dto.Descripcion,
                dto.Año_calendario
            );

            return planRepository.Update(plan);
        }

    }
}
