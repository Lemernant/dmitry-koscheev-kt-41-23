using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.GroupsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupsByAllAsync(GroupAllFilter filter, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly UniversityDbContext _dbContext;
        public GroupService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Group[]> GetGroupsByAllAsync(GroupAllFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = _dbContext.Set<Group>().Where(w => w.Specialty.Title == filter.SpecialtyTitle && w.Course == filter.Course && w.isDeleted == filter.isDeleted).ToArrayAsync(cancellationToken);
            return groups;
        }
    }
}
