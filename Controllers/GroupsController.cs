using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Interfaces.GroupsInterfaces;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace dmitry_koscheev_kt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;
        private UniversityDbContext _context;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService, UniversityDbContext context)
        {
            _logger = logger;
            _groupService = groupService;
            _context = context;
        }

        [HttpGet("by_specialty", Name = "GetGroupsBySpecialty")]
        public async Task<IActionResult> GetGroupsBySpecialtyAsync([FromQuery]GroupSpecialtyFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsBySpecialtyAsync(filter, cancellationToken);

            return Ok(groups);
        }
        
        [HttpGet("by_course", Name = "GetGroupsByCourse")]
        public async Task<IActionResult> GetGroupsByCourseAsync([FromQuery]GroupCourseFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsByCourseAsync(filter, cancellationToken);

            return Ok(groups);
        }
        
        [HttpGet("by_isdeleted", Name = "GetGroupsByIsDeleted")]
        public async Task<IActionResult> GetGroupsByIsDeletedAsync([FromQuery]GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsByIsDeletedAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpPost("create", Name = "CreateGroup")]
        public async Task<IActionResult> AddGroupAsync([FromBody] CreateGroupParams data, CancellationToken cancellationToken = default)
        {
            var group = new Group()
            {
                GroupName = data.GroupName,
                Course = data.Course,
                SpecialtyId = data.SpecialtyId
            };
            var newGroup = await _groupService.AddGroupAsync(group, cancellationToken);

            return Ok(newGroup);
        }

        [HttpPut("update", Name = "UpdateGroup")]
        public async Task<IActionResult> EditGroupAsync([FromBody] UpdateGroupParams data, CancellationToken cancellationToken = default)
        {
            var group = new Group()
            {
                GroupId = data.GroupId,
                GroupName = data.GroupName,
                Course = data.Course,
                SpecialtyId = data.SpecialtyId
            };
            var updated = await _groupService.EditGroupAsync(group, cancellationToken);
            if (updated == null) return NotFound();

            return Ok(new UpdateGroupParams(updated.GroupId, updated.GroupName, updated.Course, updated.SpecialtyId));
        }

        [HttpDelete("delete/{id}", Name = "DeleteGroup")]
        public async Task<IActionResult> DeleteGroupAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _groupService.DeleteGroupAsync(id, cancellationToken);

            if (!deleted)
                return NotFound($"Студент с Id={id} не найден");

            return NoContent();
        }

        public record CreateGroupParams(string GroupName, int Course, int SpecialtyId);
        public record UpdateGroupParams(int GroupId, string? GroupName, int? Course, int? SpecialtyId);
    }
}
