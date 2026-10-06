using dmitry_koscheev_kt_41_23.Database.Helpers;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dmitry_koscheev_kt_41_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            //Задаём первичный ключ
            builder
                .HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            //Для целочисленного первичного ключа задаём автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.DisciplineId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.DisciplineId)
                .HasColumnName("discipline_id")
                .HasComment("Идентификатор записи дисциплины");

            //HasComment добавит комментарий который будет отображаться в СУБД
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_discipline_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название дисциплины");

            builder.Property(p => p.isDeleted)
                .IsRequired()
                .HasColumnName("c_discipline_isDeleted")
                .HasColumnType(ColumnType.Bool)
                .HasComment("Удалёна ли дисциплина");

            builder.ToTable(TableName);
        }
    }
}
