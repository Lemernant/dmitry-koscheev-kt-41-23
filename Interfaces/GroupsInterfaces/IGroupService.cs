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
        public Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken);
        public Task<Group?> EditGroupAsync(Group group, CancellationToken cancellationToken);
        public Task<bool> DeleteGroupAsync(int groupid, CancellationToken cancellationToken);
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
            var groups = _dbContext.Set<Group>().Where(w => w.Specialty.Title == filter.SpecialtyTitle && !w.isDeleted)
                .Select(g => new Group
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    Course = g.Course,
                    SpecialtyId = g.SpecialtyId,
                    isDeleted = g.isDeleted,
                    Specialty = null,
                    Students = null
                })
                .ToArrayAsync(cancellationToken);
            return groups;
        }
        public Task<Group[]> GetGroupsByCourseAsync(GroupCourseFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = _dbContext.Set<Group>().Where(w => w.Course == filter.Course && !w.isDeleted)
                .Select(g => new Group
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    Course = g.Course,
                    SpecialtyId = g.SpecialtyId,
                    isDeleted = g.isDeleted,
                    Specialty = null,
                    Students = null
                })
                .ToArrayAsync(cancellationToken);
            return groups;
        }
        public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = _dbContext.Set<Group>().Where(w => w.isDeleted == filter.isDeleted)
                .Select(g => new Group
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    Course = g.Course,
                    SpecialtyId = g.SpecialtyId,
                    isDeleted = g.isDeleted,
                    Specialty = null,
                    Students = null
                })
                .ToArrayAsync(cancellationToken);
            return groups;
        }

        public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            group.isDeleted = false;
            await _dbContext.Groups.AddAsync(group, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Group?> EditGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Groups.FirstOrDefaultAsync(s => s.GroupId == group.GroupId, cancellationToken);
            if (existing == null)
            {
                return null;
            }
            existing.GroupName = group.GroupName;
            existing.Course = group.Course;
            existing.SpecialtyId = group.SpecialtyId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return existing;
        }

        public async Task<bool> DeleteGroupAsync(int groupid, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Groups.FirstOrDefaultAsync(s => s.GroupId == groupid, cancellationToken);
            if (group == null || group.isDeleted == true)
            {
                return false;
            }

            group.isDeleted = true;

            var students = await _dbContext.Students
                .Where(s => s.GroupId == groupid && !s.isDeleted)
                .ToListAsync(cancellationToken);

            foreach (var s in students)
                s.isDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
