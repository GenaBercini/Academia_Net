using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class SpecialtyService
    {
        public SpecialtyDTO Add(SpecialtyDTO dto)
        {
            var specialtyRepository = new SpecialtyRepository();

            var specialty = new Specialty(
                0,
                dto.DescEspecialidad,
                dto.DuracionAnios
            );

            specialtyRepository.Add(specialty);

            dto.Id = specialty.Id;
            return dto;
        }

        public bool Delete(int id)
        {
            var specialtyRepository = new SpecialtyRepository();
            var specialty = specialtyRepository.Get(id);

            if (specialty == null)
                return false;

            specialty.Habilitado = false;
            return specialtyRepository.Update(specialty);
        }

        public SpecialtyDTO? Get(int id)
        {
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

            var specialty = new Specialty(
                dto.Id,
                dto.DescEspecialidad,
                dto.DuracionAnios
            )
            {
                Habilitado = dto.Habilitado
            };

            return specialtyRepository.Update(specialty);
        }
    }
}

