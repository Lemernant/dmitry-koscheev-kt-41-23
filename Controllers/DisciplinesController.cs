using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.DisciplineFilters;
using dmitry_koscheev_kt_41_23.Interfaces.DisciplinesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace dmitry_koscheev_kt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplineService _disciplineService;
        private UniversityDbContext _context;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineService disciplineService, UniversityDbContext context)
        {
            _logger = logger;
            _disciplineService = disciplineService;
            _context = context;
        }

        [HttpPost("by_name", Name = "GetDisciplinesByName")]
        public async Task<IActionResult> GetDisciplinesByNameAsync(DisciplineNameFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _disciplineService.GetDisciplinesByNameAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpPost("by_isdeleted", Name = "GetDisciplinesByIsDeleted")]
        public async Task<IActionResult> GetDisciplinesByIsDeleted(DisciplineIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _disciplineService.GetDisciplinesByIsDeletedAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
