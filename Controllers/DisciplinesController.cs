using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.DisciplineFilters;
using dmitry_koscheev_kt_41_23.Interfaces.DisciplinesInterfaces;
using dmitry_koscheev_kt_41_23.Models;
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

        [HttpGet("by_name", Name = "GetDisciplinesByName")]
        public async Task<IActionResult> GetDisciplinesByNameAsync([FromQuery]DisciplineNameFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _disciplineService.GetDisciplinesByNameAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpGet("by_isdeleted", Name = "GetDisciplinesByIsDeleted")]
        public async Task<IActionResult> GetDisciplinesByIsDeleted([FromQuery]DisciplineIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _disciplineService.GetDisciplinesByIsDeletedAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpPost("create", Name = "CreateDiscipline")]
        public async Task<IActionResult> AddDisciplineAsync([FromBody] CreateDisciplineParams data, CancellationToken cancellationToken = default)
        {
            var discipline = new Discipline()
            {
                Name = data.Name
            };
            var newDiscipline = await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);

            return Ok(newDiscipline);
        }

        [HttpPut("update", Name = "UpdateDiscipline")]
        public async Task<IActionResult> EditDisciplineAsync([FromBody] UpdateDisciplineParams data, CancellationToken cancellationToken = default)
        {
            var discipline = new Discipline()
            {
                DisciplineId = data.DisciplineId,
                Name = data.Name,
            };
            var updated = await _disciplineService.EditDisciplineAsync(discipline, cancellationToken);

            return Ok(updated);
        }

        [HttpDelete("delete/{id}", Name = "DeleteDiscipline")]
        public async Task<IActionResult> DeleteDisciplineAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _disciplineService.DeleteDisciplineAsync(id, cancellationToken);

            if (!deleted)
                return NotFound($"Дисциплина с Id={id} не найден");

            return NoContent();
        }

        public record CreateDisciplineParams(string Name);
        public record UpdateDisciplineParams(int DisciplineId, string Name);
    }
}
