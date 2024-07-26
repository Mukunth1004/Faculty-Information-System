using BUTPFIS.web.Data;
using BUTPFIS.web.Models.Domain;
using BUTPFIS.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BUTPFIS.web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FISDbContext fISDbContext;
        private readonly AuthDbContext authDbContext;

        public UserRepository(FISDbContext fISDbContext, AuthDbContext authDbContext)
        {
            this.fISDbContext = fISDbContext;
            this.authDbContext = authDbContext;
        }


        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var adminUser = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "admin@bdu.com");

            if (adminUser is not null)
            {
                users.Remove(adminUser);
            }

            return users;
        }

        public async Task<FacultyInfo?> GetAsync(string email)
        {
            return await fISDbContext.FacultyInfos
                .Include(f => f.CourseInfos) // Eagerly load CourseInfos
                .FirstOrDefaultAsync(f => f.Email == email);
        }


        public async Task<FacultyInfo?> UpdateAsync(FacultyInfo facultyInfo)
        {
            var existingFaculty = await fISDbContext.FacultyInfos.Include(x => x.CourseInfos)
                .FirstOrDefaultAsync(x => x.FId == facultyInfo.FId);

            if (existingFaculty != null)
            {
                existingFaculty.Name = facultyInfo.Name;
                existingFaculty.Designation = facultyInfo.Designation;
                existingFaculty.MobileNo = facultyInfo.MobileNo;
                existingFaculty.MobileNo = facultyInfo.MobileNo;
                existingFaculty.Email = facultyInfo.Email;
                existingFaculty.FacultyImageUrl = facultyInfo.FacultyImageUrl;
                existingFaculty.PersonalInfo = facultyInfo.PersonalInfo;
                existingFaculty.Expertise = facultyInfo.Expertise;
                existingFaculty.Experience = facultyInfo.Experience;
                existingFaculty.Education = facultyInfo.Education;
                existingFaculty.Honours = facultyInfo.Honours;
                existingFaculty.Patents = facultyInfo.Patents;
                existingFaculty.Publications = facultyInfo.Publications;
                existingFaculty.Seminar = facultyInfo.Seminar;
                existingFaculty.CourseInfos = facultyInfo.CourseInfos;

                await fISDbContext.SaveChangesAsync();

                return existingFaculty;
            }

            return null;
        }

    }
}