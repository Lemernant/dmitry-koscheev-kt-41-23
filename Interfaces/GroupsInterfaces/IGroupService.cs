using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.GroupsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupsBySpecialtyAsync(GroupSpecialtyFilter filter, CancellationToken cancellationToken);
        public Task<Group[]> GetGroupsByCourseAsync(GroupCourseFilter filter, CancellationToken cancellationToken);
        public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly UniversityDbContext _dbContext;
        public GroupService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Group[]> GetGroupsBySpecialtyAsync(GroupSpecialtyFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = _dbContext.Set<Group>().Where(w => w.Specialty.Title == filter.SpecialtyTitle && !w.isDeleted).ToArrayAsync(cancellationToken);
            return groups;
        }
        public Task<Group[]> GetGroupsByCourseAsync(GroupCourseFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = _dbContext.Set<Group>().Where(w => w.Course == filter.Course && !w.isDeleted).ToArrayAsync(cancellationToken);
            return groups;
        }
        public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = _dbContext.Set<Group>().Where(w => w.isDeleted == filter.isDeleted).ToArrayAsync(cancellationToken);
            return groups;
        }
    }
}
