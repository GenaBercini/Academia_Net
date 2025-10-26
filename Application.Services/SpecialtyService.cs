using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class SpecialtyService
    {
        private readonly SpecialtyRepository _specialtyRepository;

        public SpecialtyService(SpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }
        private void ValidarSpecialtyDTO(SpecialtyDTO dto, bool isUpdate = false)
        {
            if (dto == null)
                throw new ArgumentException("Los datos de la especialidad no pueden ser nulos.");

            dto.DescEspecialidad = (dto.DescEspecialidad ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(dto.DescEspecialidad))
                throw new ArgumentException("La descripción es obligatoria.");

            if (dto.DescEspecialidad.Length < 3)
                throw new ArgumentException("La descripción debe tener al menos 3 caracteres.");

            if (dto.DuracionAnios <= 0 || dto.DuracionAnios > 100)
                throw new ArgumentException("La duración debe estar entre 1 y 100 años.");
        }
        public async Task <SpecialtyDTO> AddAsync(SpecialtyDTO dto)
        {
            ValidarSpecialtyDTO(dto);
            //var specialtyRepository = new SpecialtyRepository();

            var existing = (await _specialtyRepository.GetAllAsync())
                       .FirstOrDefault(s => s.DescEspecialidad.Equals(dto.DescEspecialidad, StringComparison.OrdinalIgnoreCase) && !s.IsDeleted);
            if (existing != null)
                throw new ArgumentException("Ya existe una especialidad con esa descripción.");

            Specialty specialty = new Specialty(0, dto.DescEspecialidad, dto.DuracionAnios);
            await _specialtyRepository.AddAsync(specialty);
            dto.Id = specialty.Id;
            return dto;
        }

  
        public async Task<bool> DeleteAsync(int id)
        {
            //var specialtyRepository = new SpecialtyRepository();
            var specialty = await _specialtyRepository.GetAsync(id);
            if (specialty == null)
                return false;
            return await _specialtyRepository.DeleteAsync(id);
        }
  

        public async Task<SpecialtyDTO> GetAsync(int id)
        {
            //var specialtyRepository = new SpecialtyRepository();
            Specialty? specialty = await _specialtyRepository.GetAsync(id);

            if (specialty == null || !specialty.IsDeleted)
                return null;

            return new SpecialtyDTO
            {
                Id = specialty.Id,
                DescEspecialidad = specialty.DescEspecialidad,
                DuracionAnios = specialty.DuracionAnios,
            };
        }


        public async Task<IEnumerable<SpecialtyDTO>> GetAllAsync()
        {
            //var specialtyRepository = new SpecialtyRepository();
            var specialties = await _specialtyRepository.GetAllAsync();

            return specialties
                .Where(s => !s.IsDeleted)
                .Select(specialty => new SpecialtyDTO
                {
                    Id = specialty.Id,
                    DescEspecialidad = specialty.DescEspecialidad,
                    DuracionAnios = specialty.DuracionAnios,
                }).ToList();
        }




        public async Task<bool> UpdateAsync(SpecialtyDTO dto)
        {
            //var specialtyRepository = new SpecialtyRepository();
            var existing = await _specialtyRepository.GetAsync(dto.Id);
            if (existing == null || existing.IsDeleted)
                throw new ArgumentException("La especialidad no existe o esta deshabilitada.");
            ValidarSpecialtyDTO(dto, isUpdate: true);
            var duplicate = (await _specialtyRepository.GetAllAsync())
                .FirstOrDefault(s =>
                    s.DescEspecialidad.Equals(dto.DescEspecialidad, StringComparison.OrdinalIgnoreCase) &&
                    s.Id != dto.Id &&
                    !s.IsDeleted);
            if (duplicate != null)
                throw new ArgumentException("Ya existe un especialidad con esa descripcion.");
            var specialty = new Specialty(
                id: dto.Id,
                descEspecialidad: dto.DescEspecialidad,
                duracionAnios: dto.DuracionAnios
            );
            specialty.IsDeleted = existing.IsDeleted;
            return await _specialtyRepository.UpdateAsync(specialty);
        }

    }
}

