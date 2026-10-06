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
        private readonly ILogger<StudentsController> logger;
        private readonly IStudentService _studentService;
        private UniversityDbContext _context;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            this.logger = logger;
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
