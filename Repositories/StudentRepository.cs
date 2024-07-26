using Microsoft.EntityFrameworkCore;
using StudentManagerApp.Models;

namespace StudentManagerApp.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _dbSet;

        public StudentRepository(DbContext context)
          : base(context)
        {
            _dbSet = context.Set<Student>();
        }

        public async Task<Student> GetByIdWithCourseAsync(int id)
        {
            return await _dbSet
                  .Include(s => s.Courses)
                  .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
