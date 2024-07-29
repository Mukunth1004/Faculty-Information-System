using BUTPFIS.web.Models.Domain;

namespace BUTPFIS.web.Repositories
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<FacultyInfo>> GetAllASync(
            string? searchQuery = null);

        Task<FacultyInfo?> GetAsync(Guid FId);

        Task<FacultyInfo> GetByEmailAsync(string email);

        Task<FacultyInfo> AddAsync(FacultyInfo facultyInfo);

        Task<FacultyInfo?> UpdateAsync(FacultyInfo facultyInfo);

        Task<FacultyInfo?> DeleteAsync(Guid FId);

        Task<IEnumerable<FacultyInfo>> GetFacultiesByCourseAsync(string courseName);

    }
}
