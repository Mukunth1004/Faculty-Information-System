using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BUTPFIS.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (Faculty, Admin)

            var adminRoleId = "32cac0da-074e-40d3-aedc-233c55ebbc96";
            var userRoleId = "274985be-e122-4529-becc-f371faef251c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed adminUser
            var adminId = "8beb11e0-c973-4164-8c8a-44754ca8c501";

            var adminUser = new IdentityUser
            {
                UserName = "admin@bdu.com",
                Email = "admin@bdu.com",
                NormalizedEmail = "admin@bdu.com".ToUpper(),
                NormalizedUserName = "admin@bdu.com".ToUpper(),
                Id = adminId
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "admin@BDU2024");

            builder.Entity<IdentityUser>().HasData(adminUser);

            //Add all roles to adminUser
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = adminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
