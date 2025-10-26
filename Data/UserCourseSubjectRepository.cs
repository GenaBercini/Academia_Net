using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserCourseSubjectRepository
    {
        private readonly TPIContext _context;

        public UserCourseSubjectRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task<UserCourseSubject?> GetAsync(int userId, int courseId, int subjectId)
        {
            return await _context.UsersCoursesSubjects
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId && e.SubjectId == subjectId);
        }

        public async Task<UserCourseSubject> AddAsync(UserCourseSubject enrollment)
        {
            _context.UsersCoursesSubjects.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public IEnumerable<UserCourseSubject> GetByUser(int userId)
        {
            return _context.UsersCoursesSubjects
                .Include(e => e.CourseId)
                .Include(e => e.SubjectId)
                .Where(e => e.UserId == userId)
                .ToList();
        }
    }
}
