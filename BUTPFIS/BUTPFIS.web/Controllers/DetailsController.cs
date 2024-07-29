using BUTPFIS.web.Models.ViewModels;
using BUTPFIS.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BUTPFIS.web.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IFacultyRepository facultyRepository;

        public DetailsController(IFacultyRepository facultyRepository)
        {
            this.facultyRepository = facultyRepository;
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var faculty = await facultyRepository.GetAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            var model = new FacultyDetailsViewModel
            {
                FId = faculty.FId,
                Name = faculty.Name,
                Designation = faculty.Designation,
                MobileNo = faculty.MobileNo,
                Email = faculty.Email,
                FacultyImageUrl = faculty.FacultyImageUrl,
                GoogleScholarLink = faculty.GoogleScholarLink,
                ResearchGateLink = faculty.ResearchGateLink,
                PersonalInfo = faculty.PersonalInfo,
                Expertise = faculty.Expertise,
                Experience = faculty.Experience,
                Education = faculty.Education,
                Honours = faculty.Honours,
                Patents = faculty.Patents,
                Publications = faculty.Publications,
                Seminar = faculty.Seminar,
                Courses = faculty.CourseInfos.Select(c => c.CourseName).ToList()
            };

            return View(model);
        }
    }
}
