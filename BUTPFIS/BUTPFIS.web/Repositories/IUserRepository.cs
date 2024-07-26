using BUTPFIS.web.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace BUTPFIS.web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
        Task<FacultyInfo?> GetAsync(string email);
        Task<FacultyInfo?>UpdateAsync(FacultyInfo facultyInfo);

    }
}