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

        [HttpPost("by_name", Name = "GetSpecialtysByName")]
        public async Task<IActionResult> GetSpecialtysByNameAsync(SpecialtyNameFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _specialtyService.GetSpecialtysByNameAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpPost("by_code", Name = "GetSpecialtysByCode")]
        public async Task<IActionResult> GetSpecialtysByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _specialtyService.GetSpecialtysByCodeAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
