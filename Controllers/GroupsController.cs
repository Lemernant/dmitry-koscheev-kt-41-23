using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GroupFilters;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Interfaces.GroupsInterfaces;
using dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces;
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

        [HttpPost(Name = "GetGroupsByAll")]
        public async Task<IActionResult> GetGroupsByAllAsync(GroupAllFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsByAllAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
