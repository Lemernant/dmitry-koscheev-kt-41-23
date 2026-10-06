using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbContext;
        public StudentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default) 
        { 
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupName == filter.GroupName).ToArrayAsync(cancellationToken);
            return students;
        }
    }
}
