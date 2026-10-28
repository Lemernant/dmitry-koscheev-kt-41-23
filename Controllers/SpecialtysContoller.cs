using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.SpecialtyFilters;
using dmitry_koscheev_kt_41_23.Interfaces.SpecialtysInterfaces;
using dmitry_koscheev_kt_41_23.Models;
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

        [HttpPost("create", Name = "CreateSpecialty")]
        public async Task<IActionResult> AddSpecialtyAsync([FromBody] CreateSpecialtyParams data, CancellationToken cancellationToken = default)
        {
            var specialty = new Specialty()
            {
                Title = data.Title,
                Code = data.Code
            };
            var newSpecialty = await _specialtyService.AddSpecialtyAsync(specialty, cancellationToken);

            return Ok(newSpecialty);
        }

        [HttpPut("update", Name = "UpdateSpecialty")]
        public async Task<IActionResult> EditSpecialtyAsync([FromBody] UpdateSpecialtyParams data, CancellationToken cancellationToken = default)
        {
            var specialty = new Specialty()
            {
                SpecialtyId = data.SpecialtyId,
                Title = data.Title,
                Code = data.Code
            };
            var updated = await _specialtyService.EditSpecialtyAsync(specialty, cancellationToken);

            return Ok(updated);
        }

        [HttpDelete("delete/{id}", Name = "DeleteSpecialty")]
        public async Task<IActionResult> DeleteSpecialtyAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _specialtyService.DeleteSpecialtyAsync(id, cancellationToken);

            if (!deleted)
                return NotFound($"Специальность с Id={id} не найдена");

            return NoContent();
        }

        public record CreateSpecialtyParams(string Title, string Code);
        public record UpdateSpecialtyParams(int SpecialtyId, string Title, string Code);
    }
}
