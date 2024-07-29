using BUTPFIS.web.Data;
using BUTPFIS.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BUTPFIS.web.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly FISDbContext fisDbContext;

        public FacultyRepository(FISDbContext fisDbContext)
        {
            this.fisDbContext = fisDbContext;
        }

        public async Task<FacultyInfo> AddAsync(FacultyInfo facultyInfo)
        {
            await fisDbContext.FacultyInfos.AddAsync(facultyInfo);
            await fisDbContext.SaveChangesAsync();
            return facultyInfo;
        }

        public async Task<FacultyInfo?> DeleteAsync(Guid FId)
        {
            var existingFaculty = await fisDbContext.FacultyInfos.FindAsync(FId);

            if (existingFaculty != null)
            {
                fisDbContext.FacultyInfos.Remove(existingFaculty);
                await fisDbContext.SaveChangesAsync();

                return existingFaculty;
            }

            return null;
        }

        public async Task<IEnumerable<FacultyInfo>> GetAllASync(string? searchQuery)
        {
            var query = fisDbContext.FacultyInfos.AsQueryable();

            // Filtering 
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery));
            }

            return await query.Include(x => x.CourseInfos).ToListAsync();

            //return await fisDbContext.FacultyInfos.ToListAsync();
        }

        public Task<FacultyInfo?> GetAsync(Guid FId)
        {
            return fisDbContext.FacultyInfos.Include(x => x.CourseInfos).FirstOrDefaultAsync(x => x.FId == FId);
        }

        public async Task<FacultyInfo?> GetByEmailAsync(string email)
        {
            return await fisDbContext.FacultyInfos.Include(x => x.CourseInfos).FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<FacultyInfo?> UpdateAsync(FacultyInfo facultyInfo)
        {
            var existingFaculty = await fisDbContext.FacultyInfos.Include(x => x.CourseInfos)
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
                existingFaculty.GoogleScholarLink = facultyInfo.GoogleScholarLink;
                existingFaculty.ResearchGateLink = facultyInfo.ResearchGateLink;
                existingFaculty.Expertise = facultyInfo.Expertise;
                existingFaculty.Experience = facultyInfo.Experience;
                existingFaculty.Education = facultyInfo.Education;
                existingFaculty.Honours = facultyInfo.Honours;
                existingFaculty.Patents = facultyInfo.Patents;
                existingFaculty.Publications = facultyInfo.Publications;
                existingFaculty.Seminar = facultyInfo.Seminar;
                existingFaculty.CourseInfos = facultyInfo.CourseInfos;

                await fisDbContext.SaveChangesAsync();

                return existingFaculty;
            }

            return null;
        }

        public async Task<IEnumerable<FacultyInfo>> GetFacultiesByCourseAsync(string courseName)
        {
            return await fisDbContext.FacultyInfos
                .Where(f => f.CourseInfos.Any(c => c.CourseName == courseName))
                .ToListAsync();
        }
    }
}
