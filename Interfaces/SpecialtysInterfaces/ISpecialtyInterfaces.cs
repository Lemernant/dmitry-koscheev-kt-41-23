using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Filters.SpecialtyFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace dmitry_koscheev_kt_41_23.Interfaces.SpecialtysInterfaces
{
    public interface ISpecialtyService
    {
        public Task<Specialty[]> GetSpecialtysByNameAsync(SpecialtyNameFilter filter, CancellationToken cancellationToken);
        public Task<Specialty[]> GetSpecialtysByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken);
        public Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken);
        public Task<Specialty?> EditSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken);
        public Task<bool> DeleteSpecialtyAsync(int specialtyid, CancellationToken cancellationToken);
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

        public async Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default)
        {
            await _dbContext.Specialtys.AddAsync(specialty, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return specialty;
        }

        public async Task<Specialty?> EditSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Specialtys.FirstOrDefaultAsync(s => s.SpecialtyId == specialty.SpecialtyId, cancellationToken);
            if (existing == null)
            {
                return null;
            }
            existing.Title = specialty.Title;
            existing.Code = specialty.Code;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return existing;
        }

        public async Task<bool> DeleteSpecialtyAsync(int specialtyid, CancellationToken cancellationToken = default)
        {
            var specialty = await _dbContext.Specialtys.FirstOrDefaultAsync(s => s.SpecialtyId == specialtyid, cancellationToken);
            if (specialty == null)
            {
                return false;
            }

            var hasGroups = await _dbContext.Groups
            .AnyAsync(s => s.SpecialtyId == specialtyid, cancellationToken);

            if (hasGroups)
                throw new InvalidOperationException(
                    $"Нельзя удалить специальность Id={specialtyid}: она содержит группы.");

            _dbContext.Specialtys.Remove(specialty);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
