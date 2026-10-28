using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.DisciplineFilters;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.DisciplinesInterfaces
{
    public interface IDisciplineService
    {
        public Task<Discipline[]> GetDisciplinesByNameAsync(DisciplineNameFilter filter, CancellationToken cancellationToken);
        public Task<Discipline[]> GetDisciplinesByIsDeletedAsync(DisciplineIsDeletedFilter filter, CancellationToken cancellationToken);

    }

    public class DisciplineService : IDisciplineService
    {
        private readonly UniversityDbContext _dbContext;
        public DisciplineService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Discipline[]> GetDisciplinesByNameAsync(DisciplineNameFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = _dbContext.Set<Discipline>().Where(w => w.Name == filter.DisciplineName && !w.isDeleted)
                .Select(d => new Discipline
                {
                    DisciplineId = d.DisciplineId,
                    Name = d.Name,
                    isDeleted = d.isDeleted,
                    Grades = null
                })
                .ToArrayAsync(cancellationToken);
            return disciplines;
        }

        public Task<Discipline[]> GetDisciplinesByIsDeletedAsync(DisciplineIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = _dbContext.Set<Discipline>().Where(w => w.isDeleted == filter.isDeleted)
                .Select(d => new Discipline
                {
                    DisciplineId = d.DisciplineId,
                    Name = d.Name,
                    isDeleted = d.isDeleted,
                    Grades = null
                })
                .ToArrayAsync(cancellationToken);
            return disciplines;
        }
    }
}
