using BUTPFIS.web.Models.Domain;

namespace BUTPFIS.web.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseInfo>> GetAllASync(
            string? searchQuery = null);

        Task<CourseInfo?> GetAsync(Guid CourseId);

        Task<CourseInfo> AddAsync(CourseInfo courseInfo);

        Task<CourseInfo?> UpdateAsync(CourseInfo courseInfo);

        Task<CourseInfo?> DeleteAsync(Guid CourseId);
    }
}
