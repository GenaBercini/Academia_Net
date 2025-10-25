using Domain.Model;
using Data;
using DTOs;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class SubjectService
    {
        private void ValidarSubjectDTO(SubjectDTO dto, bool isUpdate = false)
        {
            if (dto == null)
                throw new ArgumentException("Los datos de la materia no pueden ser nulos.");

            dto.Desc = (dto.Desc ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(dto.Desc))
                throw new ArgumentException("La descripción es obligatoria.");

            if (dto.Desc.Length < 3)
                throw new ArgumentException("La descripción debe tener al menos 3 caracteres.");

            if (!Regex.IsMatch(dto.Desc, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new ArgumentException("La descripción solo puede contener letras, números y espacios.");

            if (dto.HsSemanales <= 0 || dto.HsSemanales > 100)
                throw new ArgumentException("Las horas semanales deben estar entre 1 y 100.");
        }
        public async Task<SubjectDTO> AddAsync(SubjectDTO dto)
        {
            ValidarSubjectDTO(dto);
            var subjectRepository = new SubjectRepository();

            var existing = (await subjectRepository.GetAllAsync())
                .FirstOrDefault(s =>
                    s.Desc.Equals(dto.Desc, StringComparison.OrdinalIgnoreCase)
                    && s.IsDeleted);

            if (existing != null)
                throw new ArgumentException("Ya existe una materia con esa descripción.");

            var subject = new Subject(
                id: 0,
                desc: dto.Desc,
                hsSemanales: dto.HsSemanales,
                obligatoria: dto.Obligatoria,
                año: dto.Año,
                planId: dto.PlanId
            );

            await subjectRepository.AddAsync(subject);
            dto.Id = subject.Id;
            return dto;
        }
       

 
        public async Task<bool> DeleteAsync(int id)
        {
            var subjectRepository = new SubjectRepository();
            var subject = await subjectRepository.GetAsync(id);
            if (subject == null)
                return false;
            return await subjectRepository.DeleteAsync(id);
        }


        public async Task<SubjectDTO?> GetAsync(int id)
        {
            var subjectRepository = new SubjectRepository();
            Subject? subject = await subjectRepository.GetAsync(id);

            if (subject == null || !subject.IsDeleted)
                return null;
            
            return new SubjectDTO
            {
                Id = subject.Id,
                Desc = subject.Desc,
                HsSemanales = subject.HsSemanales,
                Obligatoria = subject.Obligatoria,
                PlanId = subject.PlanId
            };
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            var subjectRepository = new SubjectRepository();
            var subjects = await subjectRepository.GetAllAsync();

            return subjects
                .Where(s => !s.IsDeleted)
                .Select(subject => new SubjectDTO
                {
                    Id = subject.Id,
                    Desc = subject.Desc,
                    HsSemanales = subject.HsSemanales,
                    Obligatoria = subject.Obligatoria,
                    Año= subject.Año,
                    PlanId = subject.PlanId,
                }).ToList();
        }

        public async Task<bool> UpdateAsync(SubjectDTO dto)
        {
            var subjectRepository = new SubjectRepository();
            var existing = await subjectRepository.GetAsync(dto.Id);
            if (existing == null || existing.IsDeleted)
                throw new ArgumentException("La materia no existe o está deshabilitada.");
            ValidarSubjectDTO(dto, isUpdate: true);
            var duplicate = (await subjectRepository.GetAllAsync())
                .FirstOrDefault(s =>
                    s.Desc.Equals(dto.Desc, StringComparison.OrdinalIgnoreCase) &&
                    s.Id != dto.Id &&
                    !s.IsDeleted);
            if (duplicate != null)
                throw new ArgumentException("Ya existe una materia con esa descripción.");
            var subject = new Subject(
                id: dto.Id,
                desc: dto.Desc,
                hsSemanales: dto.HsSemanales,
                obligatoria: dto.Obligatoria,
                año: dto.Año,
                planId: dto.PlanId
            );
            subject.IsDeleted = existing.IsDeleted;
            return await subjectRepository.UpdateAsync(subject);
        }
    }
}
