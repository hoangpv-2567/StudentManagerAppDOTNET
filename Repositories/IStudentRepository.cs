using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagerApp.Models;

namespace StudentManagerApp.Repositories
{
    public interface IStudentRepository: IRepository<Student>
    {
        Task<Student> GetByIdWithCourseAsync(int id);
    }
}
