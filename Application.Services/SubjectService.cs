using Data;
using Domain.Model;
using DTOs;
using QuestPDF.Infrastructure;
using System.Text.RegularExpressions;
using System.Numerics;

namespace Application.Services
{
    public class SubjectService
    {
        private readonly SubjectRepository _subjectRepository;
        private readonly PlanRepository _planRepository;

        public SubjectService(SubjectRepository subjectRepository, PlanRepository planRepository)
        {
            _subjectRepository = subjectRepository;
            _planRepository = planRepository;
        }
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
            //var subjectRepository = new SubjectRepository();

            var existing = (await _subjectRepository.GetAllAsync())
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

            await _subjectRepository.AddAsync(subject);
            dto.Id = subject.Id;
            return dto;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            //var subjectRepository = new SubjectRepository();
            var subject = await _subjectRepository.GetAsync(id);
            if (subject == null)
                return false;
            return await _subjectRepository.DeleteAsync(id);
        }


        public async Task<SubjectDTO?> GetAsync(int id)
        {
            //var subjectRepository = new SubjectRepository();
            Subject? subject = await _subjectRepository.GetAsync(id);

            if (subject == null || subject.IsDeleted)
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
            var plans = await _planRepository.GetAllAsync();
            var subjects = await _subjectRepository.GetAllAsync();
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
                    PlanDescripcion = plans.FirstOrDefault(p => p.Id == subject.PlanId)?.Descripcion
                }).ToList();
        }

        public async Task<bool> UpdateAsync(SubjectDTO dto)
        {
            //var subjectRepository = new SubjectRepository();
            var existing = await _subjectRepository.GetAsync(dto.Id);
            if (existing == null || existing.IsDeleted)
                throw new ArgumentException("La materia no existe o está deshabilitada.");
            ValidarSubjectDTO(dto, isUpdate: true);
            var duplicate = (await _subjectRepository.GetAllAsync())
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
            return await _subjectRepository.UpdateAsync(subject);
        }
    }
}
