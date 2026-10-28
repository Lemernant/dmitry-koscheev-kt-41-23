using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using dmitry_koscheev_kt_41_23.Models;

namespace dmitry_koscheev_kt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        private UniversityDbContext _context;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, UniversityDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }

        [HttpGet("by_groupname", Name = "GetStudentsByGroup")]
        public async Task <IActionResult> GetStudentsByGroupAsync([FromQuery] StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpGet("by_lastname", Name = "GetStudentsByLastname")]
        public async Task<IActionResult> GetStudentsByLastnameAsync([FromQuery] StudentLastnameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByLastnameAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpGet("by_isdeleted", Name = "GetStudentsByIsDeleted")]
        public async Task<IActionResult> GetStudentsByIsDeletedAsync([FromQuery] StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByIsDeletedAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("create", Name = "CreateStudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] CreateStudentParams data, CancellationToken cancellationToken = default)
        {
            var student = new Student() 
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                GroupId = data.GroupId
            };
            var newStudent= await _studentService.AddStudentAsync(student, cancellationToken);

            return Ok(newStudent);
        }

        [HttpPut("update", Name = "UpdateStudent")]
        public async Task<IActionResult> EditStudentAsync([FromBody] UpdateStudentParams data, CancellationToken cancellationToken = default)
        {
            var student = new Student()
            {
                StudentId = data.StudentId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                GroupId = data.GroupId
            };
            var updated = await _studentService.EditStudentAsync(student, cancellationToken);

            return Ok(updated);
        }

        [HttpDelete("delete/{id}", Name = "DeleteStudent")]
        public async Task<IActionResult> DeleteStudentAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _studentService.DeleteStudentAsync(id, cancellationToken);

            if (!deleted)
                return NotFound($"Студент с Id={id} не найден");

            return NoContent();
        }

        public record CreateStudentParams(string FirstName, string LastName, int GroupId);
        public record UpdateStudentParams(int StudentId, string FirstName, string LastName, int GroupId);
    }
}
