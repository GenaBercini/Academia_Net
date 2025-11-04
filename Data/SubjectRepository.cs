using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Data
{
    public class SubjectRepository
    {

        private readonly TPIContext _context;

        public SubjectRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Subject subject)
        {
            //using var context = CreateContext();
            bool planExists = _context.Plans.Any(s => s.Id == subject.PlanId);
            if (!planExists)
                throw new InvalidOperationException($"No existe el plan con Id {subject.PlanId}");
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }
 
        public async Task<Subject?> GetAsync(int id)
        {
            //using var context = CreateContext();
            return await _context.Subjects
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            //using var context = CreateContext();
            return await _context.Subjects
                .Where(p => !p.IsDeleted)
                .Include(p => p.Plan)
                .ToListAsync();
        }
 

        public async Task<bool> UpdateAsync(Subject subject)
        {
            //using var context = CreateContext();
            var existingSubject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.Id == subject.Id);

            if (existingSubject != null && !existingSubject.IsDeleted)
            {
                existingSubject.SetDesc(subject.Desc);
                existingSubject.SetHsSemanales(subject.HsSemanales);
                existingSubject.SetObligatoria(subject.Obligatoria);
                existingSubject.SetPlanId(subject.PlanId);
                existingSubject.IsDeleted = subject.IsDeleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //using var context = CreateContext();
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null && !subject.IsDeleted)
            {
                subject.IsDeleted = true;
                _context.Subjects.Update(subject); 
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Dictionary<int, string>> GetSubjectMapADOAsync()
        {
            const string sql = @"
            SELECT Id, [Desc]
            FROM Subjects
            WHERE IsDeleted = 0
            ORDER BY Id";

            var map = new Dictionary<int, string>();
            string connectionString = _context.Database.GetConnectionString();

            await using var connection = new SqlConnection(connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string desc = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1);
                map[id] = desc;
            }

            return map;
        }


    }
}

