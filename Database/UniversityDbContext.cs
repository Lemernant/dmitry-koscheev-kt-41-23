using dmitry_koscheev_kt_41_23.Database.Configurations;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Database
{
    public class UniversityDbContext : DbContext
    {
        //Добавляем таблицы
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Specialty> Specialtys { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Grade> Grades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialtyConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
        }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) 
        { 

        }
    }
}
