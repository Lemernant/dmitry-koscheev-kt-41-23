using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GradeFilters;
using dmitry_koscheev_kt_41_23.Interfaces.GradesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace dmitry_koscheev_kt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly ILogger<GradesController> _logger;
        private readonly IGradeService _gradeService;
        private UniversityDbContext _context;

        public GradesController(ILogger<GradesController> logger, IGradeService gradeService, UniversityDbContext context)
        {
            _logger = logger;
            _gradeService = gradeService;
            _context = context;
        }

        [HttpGet("by_studentid", Name = "GetGradesByStudentId")]
        public async Task<IActionResult> GetGradesByNameAsync([FromQuery] GradeByStudentIdFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _gradeService.GetGradesByStudentIdAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
