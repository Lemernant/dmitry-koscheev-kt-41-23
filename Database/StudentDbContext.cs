using Microsoft.EntityFrameworkCore;

namespace dmitry_koscheev_kt_41_23.Database
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) 
        { 
        }
    }
}
