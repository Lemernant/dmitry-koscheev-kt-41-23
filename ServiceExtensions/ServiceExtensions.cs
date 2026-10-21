using dmitry_koscheev_kt_41_23.Interfaces.DisciplinesInterfaces;
using dmitry_koscheev_kt_41_23.Interfaces.GroupsInterfaces;
using dmitry_koscheev_kt_41_23.Interfaces.SpecialtysInterfaces;
using dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces;

namespace dmitry_koscheev_kt_41_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            return services;
        }
    }
}
