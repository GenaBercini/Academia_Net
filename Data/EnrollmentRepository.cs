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

        public async Task<bool> CourseSubjectExistsAsync(int courseId, int subjectId)
        {
            return await _context.CoursesSubjects
                .AnyAsync(cs => cs.CourseId == courseId && cs.SubjectId == subjectId);
        }

        public async Task<UserCourseSubject?> GetAsync(int userId, int courseId, int subjectId)
        {
            return await _context.UsersCoursesSubjects
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
        }

        public async Task<UserCourseSubject> AddAsync(UserCourseSubject enrollment)
        {
            var exists = await _context.UsersCoursesSubjects
                .AnyAsync(e => e.UserId == enrollment.UserId &&
                               e.CourseId == enrollment.CourseId &&
                               e.SubjectId == enrollment.SubjectId);
            if (exists)
                return enrollment;

            _context.UsersCoursesSubjects.Add(enrollment);
            try
            {
                await _context.SaveChangesAsync();
                return enrollment;
            }
            catch (DbUpdateException dbEx)
            {
                var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new InvalidOperationException($"Error al guardar la inscripción en la BD: {inner}", dbEx);
            }
        }

        public async Task<List<UserCourseSubject>> GetByUserAsync(int userId)
        {
            return await _context.UsersCoursesSubjects
                .Include(e => e.Course)
                .Include(e => e.Subject)
                .Include(e => e.User)
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<UserCourseSubject>> GetByUserAndCourseAsync(int userId, int courseId)
        {
            return await _context.UsersCoursesSubjects
                .Where(u => u.UserId == userId && u.CourseId == courseId)
                .ToListAsync();
        }

        public void Delete(UserCourseSubject enrollment)
        {
            _context.UsersCoursesSubjects.Remove(enrollment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
