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

        [HttpPost("by_groupname", Name = "GetStudentsByGroup")]
        public async Task <IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("by_lastname", Name = "GetStudentsByLastname")]
        public async Task<IActionResult> GetStudentsByLastnameAsync(StudentLastnameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByLastnameAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("by_isdeleted", Name = "GetStudentsByIsDeleted")]
        public async Task<IActionResult> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByIsDeletedAsync(filter, cancellationToken);

            return Ok(students);
        }
    }
}
