using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GradeFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.GradesInterfaces
{
    public interface IGradeService
    {
        public Task<Grade[]> GetGradesByStudentIdAsync(GradeByStudentIdFilter filter, CancellationToken cancellationToken);

    }

    public class GradeService : IGradeService
    {
        private readonly UniversityDbContext _dbContext;
        public GradeService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Grade[]> GetGradesByStudentIdAsync(GradeByStudentIdFilter filter, CancellationToken cancellationToken = default)
        {
            var grades = _dbContext.Set<Grade>().Where(w => w.StudentId == filter.StudentId)
                .Select(d => new Grade
                {
                    GradeId = d.GradeId,
                    Value = d.Value,
                    StudentId = d.StudentId,
                    DisciplineId = d.DisciplineId,
                    Student = null
                })
                .ToArrayAsync(cancellationToken);
            return grades;
        }
    }
}
