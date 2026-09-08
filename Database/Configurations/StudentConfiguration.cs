using dmitry_koscheev_kt_41_23.Database.Helpers;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dmitry_koscheev_kt_41_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student> 
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "koshcd_student";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Задаём первичный ключ
            builder
                .HasKey(p => p.StudentId)
                .HasName($"pk_{TableName}_student_id");
            
            //Для целочисленного первичного ключа задаём автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.StudentId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.StudentId)
                .HasColumnName("student_id")
                .HasComment("Идентификатор записи студента");

            //HasComment добавит комментарий который будет отображаться в СУБД
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("c_student_firstname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Имя студента");

            builder.Property(p => p.LastName)
                .IsRequired()

            throw new NotImplementedException();
        }
    }
}
