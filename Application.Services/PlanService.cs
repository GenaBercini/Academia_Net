using Domain.Model;
using Data;
using DTOs;
using System.Numerics;

namespace Application.Services
{
    public class PlanService
    {
        private readonly PlanRepository _planRepository;

        public PlanService(PlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        private void ValidarPlanDTO(PlanDTO dto ,bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ArgumentException("La descripción es obligatoria.");

            if (dto.Descripcion.Length < 3 || dto.Descripcion.Length > 100)
                throw new ArgumentException("La descripción debe tener entre 3 y 100 caracteres.");

            int añoActual = DateTime.Now.Year;
            if (dto.Año_calendario < 2000 || dto.Año_calendario > añoActual + 1)
                throw new ArgumentException("Año calendario inválido.");

            if (dto == null)
                throw new ArgumentException("Los datos del plan no pueden ser nulos.");
        }

        public async Task<PlanDTO> AddAsync(PlanDTO dto)
        {
            //var planRepository = new PlanRepository();
            var plan = new Plan(0, dto.Descripcion, dto.Año_calendario, dto.SpecialtyId);
            await _planRepository.AddAsync(plan);
            dto.Id = plan.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var planRepository = new PlanRepository();
            var plan = await _planRepository.GetAsync(id);
            if (plan == null)
                return false;
            return await _planRepository.DeleteAsync(id);
        }
    
        public async Task<PlanDTO?> GetAsync(int id)
        {
            //var planRepository = new PlanRepository();
            Plan? plan = await _planRepository.GetAsync(id);

            if (plan == null)
                return null;

            return new PlanDTO
            {
                Id = plan.Id,
                Año_calendario = plan.Año_calendario,
                Descripcion = plan.Descripcion,
                SpecialtyId = plan.SpecialtyId,
            };
        }

        public async Task<IEnumerable<PlanDTO>> GetAllAsync()
        {
            //var planRepository = new PlanRepository();
            var plans = await _planRepository.GetAllAsync();
            return plans
                .Where(s => !s.IsDeleted)
                .Select(plan => new PlanDTO
            {
                Id = plan.Id,
                Año_calendario = plan.Año_calendario,
                Descripcion = plan.Descripcion,
                SpecialtyId = plan.SpecialtyId,
            }).ToList();
        }

        public async Task<bool> UpdateAsync(PlanDTO dto)
        {
            //var planRepository = new PlanRepository();
            var existing = await _planRepository.GetAsync(dto.Id);
            if (existing == null || existing.IsDeleted)
                throw new ArgumentException("El plan no existe o está deshabilitado.");
            ValidarPlanDTO(dto, isUpdate: true);
            var duplicate = (await _planRepository.GetAllAsync())
                .FirstOrDefault(p =>
                    p.Descripcion.Equals(dto.Descripcion, StringComparison.OrdinalIgnoreCase) &&
                    p.Año_calendario == dto.Año_calendario &&
                    p.Id != dto.Id &&
                    !p.IsDeleted);
            if (duplicate != null)
                throw new ArgumentException("Ya existe un plan con esa descripción y año.");
            var plan = new Plan(
                id: dto.Id,
                descripcion: dto.Descripcion,
                año_calendario: dto.Año_calendario,
                specialtyId: dto.SpecialtyId
            );
            plan.IsDeleted = existing.IsDeleted;
            return await _planRepository.UpdateAsync(plan);
        }

    }
}
