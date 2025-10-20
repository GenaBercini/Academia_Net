using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class SpecialtyService
    {
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

            var specialtyRepository = new SpecialtyRepository();

            var duplicado = specialtyRepository.GetAll()
                .FirstOrDefault(s =>
                    s.DescEspecialidad.Equals(dto.DescEspecialidad, StringComparison.OrdinalIgnoreCase) &&
                    s.Habilitado &&
                    (!isUpdate || s.Id != dto.Id));

            if (duplicado != null)
                throw new ArgumentException("Ya existe una especialidad con esa descripción.");
        }
        public SpecialtyDTO Add(SpecialtyDTO dto)
        {
            ValidarSpecialtyDTO(dto);
            var specialtyRepository = new SpecialtyRepository();

            var specialty = new Specialty(0, dto.DescEspecialidad, dto.DuracionAnios);
            specialtyRepository.Add(specialty);
            dto.Id = specialty.Id;

            return dto;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser mayor que cero.");

            var specialtyRepository = new SpecialtyRepository();
            var specialty = specialtyRepository.Get(id);

            if (specialty == null)
                return false;

            specialty.Habilitado = false;
            return specialtyRepository.Update(specialty);
        }

        public SpecialtyDTO? Get(int id)
        {
            if (id <= 0)
                return null;

            var specialtyRepository = new SpecialtyRepository();
            Specialty? specialty = specialtyRepository.Get(id);

            if (specialty == null || !specialty.Habilitado)
                return null;

            return new SpecialtyDTO
            {
                Id = specialty.Id,
                DescEspecialidad = specialty.DescEspecialidad,
                DuracionAnios = specialty.DuracionAnios,
                Habilitado = specialty.Habilitado
            };
        }

        public IEnumerable<SpecialtyDTO> GetAll()
        {
            var specialtyRepository = new SpecialtyRepository();
            var specialties = specialtyRepository.GetAll();

            return specialties
                .Where(s => s.Habilitado)
                .Select(specialty => new SpecialtyDTO
                {
                    Id = specialty.Id,
                    DescEspecialidad = specialty.DescEspecialidad,
                    DuracionAnios = specialty.DuracionAnios,
                    Habilitado = specialty.Habilitado
                }).ToList();
        }

        public bool Update(SpecialtyDTO dto)
        {
            var specialtyRepository = new SpecialtyRepository();
            var existing = specialtyRepository.Get(dto.Id);

            if (existing == null || !existing.Habilitado)
                throw new ArgumentException("La especialidad no existe o está deshabilitada.");

            ValidarSpecialtyDTO(dto, isUpdate: true);

            var specialty = new Specialty(dto.Id, dto.DescEspecialidad, dto.DuracionAnios)
            {
                Habilitado = dto.Habilitado
            };

            return specialtyRepository.Update(specialty);
        }

    }
}

