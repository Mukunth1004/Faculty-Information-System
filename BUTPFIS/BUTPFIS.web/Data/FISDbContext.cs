using BUTPFIS.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BUTPFIS.web.Data
{
    public class FISDbContext : DbContext
    {
        public FISDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FacultyInfo> FacultyInfos { get; set; }
    }
}