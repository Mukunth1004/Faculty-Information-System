using BUTPFIS.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BUTPFIS.web.Data
{
    public class FISDbContext : DbContext
    {
        public FISDbContext(DbContextOptions<FISDbContext> options) : base(options)
        {
        }
        public DbSet<CourseInfo> CourseInfos { get; set; }
        public DbSet<FacultyInfo> FacultyInfos { get; set; }
    }
}