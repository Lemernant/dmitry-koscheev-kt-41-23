using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Interfaces.GroupsInterfaces;
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

        [HttpPost("by_specialty", Name = "GetGroupsBySpecialty")]
        public async Task<IActionResult> GetGroupsBySpecialtyAsync(GroupSpecialtyFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsBySpecialtyAsync(filter, cancellationToken);

            return Ok(groups);
        }
        
        [HttpPost("by_course", Name = "GetGroupsByCourse")]
        public async Task<IActionResult> GetGroupsByCourseAsync(GroupCourseFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsByCourseAsync(filter, cancellationToken);

            return Ok(groups);
        }
        
        [HttpPost("by_isdeleted", Name = "GetGroupsByIsDeleted")]
        public async Task<IActionResult> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsByIsDeletedAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
