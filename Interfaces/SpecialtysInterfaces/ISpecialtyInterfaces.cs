using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.SpecialtyFilters;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.SpecialtysInterfaces
{
    public interface ISpecialtyService
    {
        public Task<Specialty[]> GetSpecialtysByNameAsync(SpecialtyNameFilter filter, CancellationToken cancellationToken);
        public Task<Specialty[]> GetSpecialtysByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken);

    }

    public class SpecialtyService : ISpecialtyService
    {
        private readonly UniversityDbContext _dbContext;
        public SpecialtyService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Specialty[]> GetSpecialtysByNameAsync(SpecialtyNameFilter filter, CancellationToken cancellationToken = default)
        {
            var specialtys = _dbContext.Set<Specialty>().Where(w => w.Title == filter.Name)
                .Select(s => new Specialty
                {
                    SpecialtyId = s.SpecialtyId,
                    Title = s.Title,
                    Code = s.Code,
                    Groups = null
                })
                .ToArrayAsync(cancellationToken);
            return specialtys;
        }

        public Task<Specialty[]> GetSpecialtysByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            var specialtys = _dbContext.Set<Specialty>().Where(w => w.Code == filter.Code)
                .Select(s => new Specialty
                {
                    SpecialtyId = s.SpecialtyId,
                    Title = s.Title,
                    Code = s.Code,
                    Groups = null
                })
                .ToArrayAsync(cancellationToken);
            return specialtys;
        }
    }
}
