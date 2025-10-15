using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class SubjectService
    {
        public SubjectDTO Add(SubjectDTO dto)
        {
            var subjectRepository = new SubjectRepository();

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
            var subjectRepository = new SubjectRepository();
            var subject = subjectRepository.Get(id);

            if (subject == null)
                return false;

            subject.Habilitado = false; // borrado lógico
            return subjectRepository.Update(subject);
        }

        public SubjectDTO? Get(int id)
        {
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
