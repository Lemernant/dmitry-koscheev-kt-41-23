using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.StudentFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        /*public Task<string> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);*/
        public Task<Student[]> GetStudentsByLastnameAsync(StudentLastnameFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken);
        public Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken);
        public Task<Student> EditStudentAsync(Student student, CancellationToken cancellationToken);

    }

    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbContext;
        public StudentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default) 
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupName == filter.GroupName && !w.isDeleted)
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    Group = null
                })
                .ToArrayAsync(cancellationToken);
            return students;
        }

        //Вывод только фамилий
        /*public async Task<string> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Student>().Where(w => w.Group.GroupName == filter.GroupName && !w.isDeleted)
                .Select(s => s.LastName)
                .ToArrayAsync(cancellationToken);
            return string.Join(", ", students);
        }*/

        public Task<Student[]> GetStudentsByLastnameAsync(StudentLastnameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.LastName == filter.Lastname && !w.isDeleted)
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    Group = null
                })
                .ToArrayAsync(cancellationToken);
            return students;
        }
        public Task<Student[]> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.isDeleted == filter.isDeleted)
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    Group = null
                })
                .ToArrayAsync(cancellationToken);
            return students;
        }

        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            student.isDeleted = false;
            await _dbContext.Students.AddAsync(student, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<Student> EditStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }
    }
}
