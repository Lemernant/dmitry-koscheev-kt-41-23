using dmitry_koscheev_kt_41_23.Database.Helpers;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dmitry_koscheev_kt_41_23.Database.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_specialty";

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            //Задаём первичный ключ
            builder
                .HasKey(p => p.SpecialtyId)
                .HasName($"pk_{TableName}_specialty_id");

            //Для целочисленного первичного ключа задаём автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.SpecialtyId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.SpecialtyId)
                .HasColumnName("specialty_id")
                .HasComment("Идентификатор записи Специальности");

            //HasComment добавит комментарий который будет отображаться в СУБД
            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("c_specialty_title")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название специальности");

            builder.Property(p => p.Code)
                    .IsRequired()
                    .HasColumnName("c_specialty_code")
                    .HasColumnType(ColumnType.String).HasMaxLength(100)
                    .HasComment("Код специальности");
            
            builder.ToTable(TableName);
        }
    }
}
