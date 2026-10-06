using dmitry_koscheev_kt_41_23.Database.Helpers;
using dmitry_koscheev_kt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dmitry_koscheev_kt_41_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //Задаём первичный ключ
            builder
                .HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            //Для целочисленного первичного ключа задаём автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.GroupId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.GroupId)
                .HasColumnName("group_id")
                .HasComment("Идентификатор записи группы");

            //HasComment добавит комментарий который будет отображаться в СУБД
            builder.Property(p => p.GroupName)
                .IsRequired()
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название группы");

            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnName("c_group_course")
                .HasColumnType(ColumnType.Int).HasMaxLength(100)
                .HasComment("Номер курса");

            builder.Property(p => p.SpecialityId)
                .IsRequired()
                .HasColumnName("f_speciality_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор специальности");

            builder.Property(p => p.isDeleted)
                .IsRequired()
                .HasColumnName("c_group_isDeleted")
                .HasColumnType(ColumnType.Bool)
                .HasComment("Удалёна ли группа");

            builder.ToTable(TableName);
        }
    }
}
