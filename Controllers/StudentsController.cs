using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace dmitry_koscheev_kt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost(Name = "GetStudentsByGroup")]
        public async Task <IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }
    }
}
