using Data;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class EnrollmentRepository
    {
        private readonly TPIContext _context;

        public EnrollmentRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task<UserCourseSubject?> GetAsync(int userId, int courseId, int subjectId)
        {
            return await _context.UsersCoursesSubjects
                .Include(e => e.User)
                .Include(e => e.Course)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e =>
                    e.UserId == userId &&
                    e.CourseId == courseId &&
                    e.SubjectId == subjectId);
        }

        public async Task<IEnumerable<UserCourseSubject>> GetByUserAsync(int userId)
        {
            return await _context.UsersCoursesSubjects
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .Include(e => e.Subject)
                .ToListAsync();
        }

        public async Task AddAsync(UserCourseSubject entity)
        {
            _context.UsersCoursesSubjects.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UserCourseSubject entity)
        {
            var existing = await GetAsync(entity.UserId, entity.CourseId, entity.SubjectId);
            if (existing == null) return false;

            existing.NotaFinal = entity.NotaFinal;
            existing.FechaInscripcion = entity.FechaInscripcion;

            await _context.SaveChangesAsync();
            return true;
        }

        public void Delete(UserCourseSubject entity)
        {
            _context.UsersCoursesSubjects.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<Course?> GetCourseAsync(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<Subject?> GetSubjectAsync(int subjectId)
        {
            return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);
        }

        public async Task<bool> CourseSubjectExistsAsync(int courseId, int subjectId)
        {
            return await _context.CoursesSubjects
                .AnyAsync(cs => cs.CourseId == courseId && cs.SubjectId == subjectId);
        }

        public async Task AddCourseSubjectAsync(CourseSubject entity)
        {
            _context.CoursesSubjects.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserCourseSubject>> GetAllAsync()
        {
            return await _context.UsersCoursesSubjects
                                 .Include(e => e.Course)
                                 .Include(e => e.Subject)
                                 .ToListAsync();
        }
    }
}
