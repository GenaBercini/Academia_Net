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
        public SubjectDTO Add(SubjectDTO dto)
        {
            ValidarSubjectDTO(dto);
            var subjectRepository = new SubjectRepository();

            var existing = subjectRepository.GetAll()
                .FirstOrDefault(s =>
                    s.Desc.Equals(dto.Desc, StringComparison.OrdinalIgnoreCase)
                    && s.Habilitado);

            if (existing != null)
                throw new ArgumentException("Ya existe una materia con esa descripción.");

            var subject = new Subject(
                id: 0,
                desc: dto.Desc,
                hsSemanales: dto.HsSemanales,
                obligatoria: dto.Obligatoria
            );

            subjectRepository.Add(subject);
            dto.Id = subject.Id;
            return dto;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser mayor que cero.");

            var subjectRepository = new SubjectRepository();
            var subject = subjectRepository.Get(id);

            if (subject == null)
                return false;

            subject.Habilitado = false; 
            return subjectRepository.Update(subject);
        }

        public SubjectDTO? Get(int id)
        {
            if (id <= 0)
                return null;

            var subjectRepository = new SubjectRepository();
            Subject? subject = subjectRepository.Get(id);

            if (subject == null || !subject.Habilitado)
                return null;

            return new SubjectDTO
            {
                Id = subject.Id,
                Desc = subject.Desc,
                HsSemanales = subject.HsSemanales,
                Obligatoria = subject.Obligatoria,
                Habilitado = subject.Habilitado
            };
        }

        public IEnumerable<SubjectDTO> GetAll()
        {
            var subjectRepository = new SubjectRepository();
            var subjects = subjectRepository.GetAll();

            return subjects
                .Where(s => s.Habilitado)
                .Select(subject => new SubjectDTO
                {
                    Id = subject.Id,
                    Desc = subject.Desc,
                    HsSemanales = subject.HsSemanales,
                    Obligatoria = subject.Obligatoria,
                    Habilitado = subject.Habilitado
                }).ToList();
        }

        public bool Update(SubjectDTO dto)
        {
            var subjectRepository = new SubjectRepository();

            var existing = subjectRepository.Get(dto.Id);
            if (existing == null || !existing.Habilitado)
                throw new ArgumentException("La materia no existe o está deshabilitada.");

            ValidarSubjectDTO(dto, isUpdate: true);

            var duplicate = subjectRepository.GetAll()
                .FirstOrDefault(s =>
                    s.Desc.Equals(dto.Desc, StringComparison.OrdinalIgnoreCase) &&
                    s.Id != dto.Id &&
                    s.Habilitado);

            if (duplicate != null)
                throw new ArgumentException("Ya existe una materia con esa descripción.");

            var subject = new Subject(
                id: dto.Id,
                desc: dto.Desc,
                hsSemanales: dto.HsSemanales,
                obligatoria: dto.Obligatoria
            )
            {
                Habilitado = dto.Habilitado
            };

            return subjectRepository.Update(subject);
        }
    }
}
