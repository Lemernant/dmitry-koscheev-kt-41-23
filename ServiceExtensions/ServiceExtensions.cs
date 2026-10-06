using dmitry_koscheev_kt_41_23.Interfaces.StudentsInterfaces;

namespace dmitry_koscheev_kt_41_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IStudentService, StudentService>();
            return services;
        }
    }
}
