using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Interfaces.SpecialtysInterfaces;
using dmitry_koscheev_kt_41_23.Filters.SpecialtyFilters;
using Microsoft.AspNetCore.Mvc;


namespace dmitry_koscheev_kt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialtysController : ControllerBase
    {
        private readonly ILogger<SpecialtysController> _logger;
        private readonly ISpecialtyService _specialtyService;
        private UniversityDbContext _context;

        public SpecialtysController(ILogger<SpecialtysController> logger, ISpecialtyService specialtyService, UniversityDbContext context)
        {
            _logger = logger;
            _specialtyService = specialtyService;
            _context = context;
        }

        [HttpGet("by_name", Name = "GetSpecialtysByName")]
        public async Task<IActionResult> GetSpecialtysByNameAsync([FromQuery] SpecialtyNameFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _specialtyService.GetSpecialtysByNameAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpGet("by_code", Name = "GetSpecialtysByCode")]
        public async Task<IActionResult> GetSpecialtysByCodeAsync([FromQuery] SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _specialtyService.GetSpecialtysByCodeAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
