using BUTPFIS.web.Data;
using BUTPFIS.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BUTPFIS.web.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FISDbContext fisDbContext;

        public CourseRepository(FISDbContext fisDbContext)
        {
            this.fisDbContext = fisDbContext;
        }

        public async Task<CourseInfo> AddAsync(CourseInfo courseInfo)
        {
            await fisDbContext.CourseInfos.AddAsync(courseInfo);
            await fisDbContext.SaveChangesAsync();
            return courseInfo;
        }

        public async Task<CourseInfo?> DeleteAsync(Guid CourseId)
        {
            var existingCourse = await fisDbContext.CourseInfos.FindAsync(CourseId);

            if (existingCourse != null)
            {
                fisDbContext.CourseInfos.Remove(existingCourse);
                await fisDbContext.SaveChangesAsync();

                return existingCourse;
            }

            return null;
        }

        public async Task<IEnumerable<CourseInfo>> GetAllASync(string? searchQuery)
        {
            var query = fisDbContext.CourseInfos.AsQueryable();

            // Filtering 
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.CourseName.Contains(searchQuery));
            }

            return await query.ToListAsync();
            
            //return await fisDbContext.CourseInfos.ToListAsync();
        }

        public Task<CourseInfo?> GetAsync(Guid CourseId)
        {
            return fisDbContext.CourseInfos.FirstOrDefaultAsync(x => x.CourseId == CourseId);
        }

        public async Task<CourseInfo?> UpdateAsync(CourseInfo courseInfo)
        {
            var existingCourse = await fisDbContext.CourseInfos.FindAsync(courseInfo.CourseId);

            if (existingCourse != null)
            {
                existingCourse.CourseName = courseInfo.CourseName;

                await fisDbContext.SaveChangesAsync();

                return existingCourse;
            }

            return null;
        }
    }
}
