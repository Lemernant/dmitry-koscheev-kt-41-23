using dmitry_koscheev_kt_41_23.Database;
using dmitry_koscheev_kt_41_23.Filters.GradeFilters;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using static dmitry_koscheev_kt_41_23.Controllers.GradesController;

namespace dmitry_koscheev_kt_41_23.Interfaces.GradesInterfaces
{
    public interface IGradeService
    {
        public Task<Grade[]> GetGradesByStudentIdAsync(GradeByStudentIdFilter filter, CancellationToken cancellationToken);
        public Task<Grade> AddGradeAsync(Grade student, CancellationToken cancellationToken);
        public Task<Grade?> EditGradeAsync(Grade student, CancellationToken cancellationToken);
        public Task<bool> DeleteGradeAsync(int studentid, CancellationToken cancellationToken);
        public Task<AverageGradeGroup[]> GetAverageByGroupAndDisciplineAsync(GradeGroupDisciplineAverageFilter filter, CancellationToken cancellationToken);
        public Task<AverageGradeCourse[]> GetAverageByCourseAsync(GradeCourseAverageFilter filter, CancellationToken cancellationToken);

    }

    public class GradeService : IGradeService
    {
        private readonly UniversityDbContext _dbContext;
        public GradeService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Grade[]> GetGradesByStudentIdAsync(GradeByStudentIdFilter filter, CancellationToken cancellationToken = default)
        {
            var grades = _dbContext.Set<Grade>().Where(w => w.StudentId == filter.StudentId)
                .Select(d => new Grade
                {
                    GradeId = d.GradeId,
                    Value = d.Value,
                    StudentId = d.StudentId,
                    DisciplineId = d.DisciplineId,
                    Student = null
                })
                .ToArrayAsync(cancellationToken);
            return grades;
        }

        public Task<AverageGradeGroup[]> GetAverageByGroupAndDisciplineAsync(GradeGroupDisciplineAverageFilter filter, CancellationToken cancellationToken) 
        {
            var query = _dbContext.Set<Grade>()
                .Where(g =>
                    !g.Student!.isDeleted &&
                    !g.Student.Group!.isDeleted &&
                    g.Student!.Group!.GroupName == filter.GroupName &&
                    g.Discipline!.Name == filter.DisciplineName);
            var res = query
                .GroupBy(g => new
                {
                    g.Student!.Group!.GroupName,
                    g.Discipline!.Name
                })
                .Select(gr => new AverageGradeGroup(
                    gr.Key.GroupName!,
                    gr.Key.Name!,
                    gr.Average(x => (double?)x.Value),
                    gr.Count()))
                .ToArrayAsync(cancellationToken);

            return res;
        }

        public Task<AverageGradeCourse[]> GetAverageByCourseAsync(GradeCourseAverageFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Grade>()
                .Where(g =>
                    !g.Student!.isDeleted &&
                    !g.Student.Group!.isDeleted &&
                    g.Student.Group.Course == filter.Course!.Value);
            var res = query
                .GroupBy(g => 
                    g.Student!.Group!.Course!.Value)
                .Select(gr => new AverageGradeCourse(
                    gr.Key,
                    gr.Average(x => (double?)x.Value),
                    gr.Count()))
                .ToArrayAsync(cancellationToken);

            return res;
        }

        public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            await _dbContext.Grades.AddAsync(grade, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

        public async Task<Grade?> EditGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Grades.FirstOrDefaultAsync(s => s.GradeId == grade.GradeId, cancellationToken);
            if (existing == null)
            {
                return null;
            }
            existing.Value = grade.Value;
            existing.StudentId = grade.StudentId;
            existing.DisciplineId = grade.DisciplineId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return existing;
        }

        public async Task<bool> DeleteGradeAsync(int gradeid, CancellationToken cancellationToken = default)
        {
            var grade = await _dbContext.Grades.FirstOrDefaultAsync(s => s.GradeId == gradeid, cancellationToken);
            if (grade == null)
            {
                return false;
            }

            _dbContext.Grades.Remove(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
