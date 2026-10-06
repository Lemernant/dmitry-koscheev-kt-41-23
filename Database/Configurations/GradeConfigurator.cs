using dmitry_koscheev_kt_41_23.Database.Helpers;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dmitry_koscheev_kt_41_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            //Задаём первичный ключ
            builder
                .HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grade_id");

            //Для целочисленного первичного ключа задаём автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.GradeId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.GradeId)
                .HasColumnName("grade_id")
                .HasComment("Идентификатор записи оценки");

            //HasComment добавит комментарий который будет отображаться в СУБД
            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("c_grade_value")
                .HasColumnType(ColumnType.Int)
                .HasComment("Значение оценки");

            builder.Property(p => p.StudentId)
                .IsRequired()
                .HasColumnName("f_student_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор студента");

            builder.Property(p => p.DisciplineId)
                .IsRequired()
                .HasColumnName("f_discipline_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор дисциплины");

            builder.HasOne(p => p.Student)
                .WithMany(t => t.Grades)
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_f_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Student)
                .AutoInclude();

            builder.HasOne(p => p.Discipline)
                .WithMany(t => t.Grades)
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Discipline)
                .AutoInclude();
        }
    }
}
