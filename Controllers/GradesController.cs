using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GradeFilters;
using dmitry_koscheev_kt_41_23.Interfaces.GradesInterfaces;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.AspNetCore.Mvc;
using static dmitry_koscheev_kt_41_23.Controllers.GroupsController;

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

        [HttpGet("avg_by_group_discipline", Name = "GetAverageGradeByGroupAndDiscipline")]
        public async Task<IActionResult> GetAverageByGroupAndDisciplineAsync([FromQuery] GradeGroupDisciplineAverageFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _gradeService.GetAverageByGroupAndDisciplineAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpGet("avg_by_course", Name = "GetAverageGradeByCourse")]
        public async Task<IActionResult> GetAverageByCourseAsync([FromQuery] GradeCourseAverageFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _gradeService.GetAverageByCourseAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpPost("create", Name = "CreateGrade")]
        public async Task<IActionResult> AddGradeAsync([FromBody] CreateGradeParams data, CancellationToken cancellationToken = default)
        {
            var grade = new Grade()
            {
                Value = data.Value,
                StudentId = data.StudentId,
                DisciplineId = data.DisciplineId
            };
            var newGrade = await _gradeService.AddGradeAsync(grade, cancellationToken);

            return Ok(newGrade);
        }

        [HttpPut("update", Name = "UpdateGrade")]
        public async Task<IActionResult> EditGradeAsync([FromBody] UpdateGradeParams data, CancellationToken cancellationToken = default)
        {
            var grade = new Grade()
            {
                GradeId = data.GradeId,
                Value = data.Value,
                StudentId = data.StudentId,
                DisciplineId = data.DisciplineId
            };
            var updated = await _gradeService.EditGradeAsync(grade, cancellationToken);

            return Ok(new UpdateGradeParams(updated.GradeId, updated.Value, updated.StudentId, updated.DisciplineId));
        }

        [HttpDelete("delete/{id}", Name = "DeleteGrade")]
        public async Task<IActionResult> DeleteGradeAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _gradeService.DeleteGradeAsync(id, cancellationToken);

            if (!deleted)
                return NotFound($"Оценка с Id={id} не найдена");

            return NoContent();
        }

        public record CreateGradeParams(int Value, int StudentId, int DisciplineId);
        public record UpdateGradeParams(int GradeId, int Value, int StudentId, int DisciplineId);
        public record AverageGradeGroup(string GroupName,
            string DisciplineName, double? Average, int Count);
        public record AverageGradeCourse(int? Course, double? Average, int Count);
    }
}
