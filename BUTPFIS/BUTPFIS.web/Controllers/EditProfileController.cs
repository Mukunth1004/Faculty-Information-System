using BUTPFIS.web.Models.Domain;
using BUTPFIS.web.Models.ViewModels;
using BUTPFIS.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace BUTPFIS.web.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IFacultyRepository facultyRepository;
        private readonly UserManager<IdentityUser> userManager;

        public EditProfileController(IUserRepository userRepository, ICourseRepository courseRepository, IFacultyRepository facultyRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.courseRepository = courseRepository;
            this.facultyRepository = facultyRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            var facultyinfo = await userRepository.GetAsync(user.Email);
            var coursesDomainModel = await courseRepository.GetAllASync(null);

            if (facultyinfo != null)
            {
                var model = new EditFaculty
                {
                    FId = facultyinfo.FId,
                    Name = facultyinfo.Name,
                    Designation = facultyinfo.Designation,
                    MobileNo = facultyinfo.MobileNo,
                    Email = facultyinfo.Email,
                    FacultyImageUrl = facultyinfo.FacultyImageUrl,
                    PersonalInfo = facultyinfo.PersonalInfo,
                    GoogleScholarLink = facultyinfo.GoogleScholarLink,
                    ResearchGateLink = facultyinfo.ResearchGateLink,
                    Expertise = facultyinfo.Expertise,
                    Experience = facultyinfo.Experience,
                    Education = facultyinfo.Education,
                    Honours = facultyinfo.Honours,
                    Patents = facultyinfo.Patents,
                    Publications = facultyinfo.Publications,
                    Seminar = facultyinfo.Seminar,
                    Courses = coursesDomainModel?.Select(x => new SelectListItem
                    {
                        Text = x.CourseName,
                        Value = x.CourseId.ToString(),
                        Selected = facultyinfo.CourseInfos.Any(c => c.CourseId == x.CourseId)
                    }) ?? Enumerable.Empty<SelectListItem>(),
                    SelectedCourses = facultyinfo.CourseInfos?.Select(x => x.CourseId.ToString()).ToArray() ?? Array.Empty<string>()
                };

                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFaculty editFaculty)
        {            
            // Map view model back to domain model
            var facultyDomainModel = new FacultyInfo
            {
                FId = editFaculty.FId,
                Name = editFaculty.Name,
                Designation = editFaculty.Designation,
                MobileNo = editFaculty.MobileNo,
                Email = editFaculty.Email,
                FacultyImageUrl = editFaculty.FacultyImageUrl,
                PersonalInfo = editFaculty.PersonalInfo,
                GoogleScholarLink = editFaculty.GoogleScholarLink,
                ResearchGateLink = editFaculty.ResearchGateLink,
                Expertise = editFaculty.Expertise,
                Experience = editFaculty.Experience,
                Education = editFaculty.Education,
                Honours = editFaculty.Honours,
                Patents = editFaculty.Patents,
                Publications = editFaculty.Publications,
                Seminar = editFaculty.Seminar
            };

            var selectedCourses = new List<CourseInfo>();
            foreach (var selectedCourse in editFaculty.SelectedCourses)
            {
                if (Guid.TryParse(selectedCourse, out var course))
                {
                    var foundCourse = await courseRepository.GetAsync(course);

                    if (foundCourse != null)
                    {
                        selectedCourses.Add(foundCourse);
                    }
                }
            }

            facultyDomainModel.CourseInfos = selectedCourses;

            var updatedFaculty = await facultyRepository.UpdateAsync(facultyDomainModel);

            if (updatedFaculty != null)
            {
                // Show success notification
                return RedirectToAction("Edit");
            }
            else
            {
                // Show error notification
                return RedirectToAction("Edit");
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditFaculty editFaculty)
        {
            var deletedFaculty = await facultyRepository.DeleteAsync(editFaculty.FId);

            if (deletedFaculty != null)
            {
                //Show success notification
                TempData["Notification"] = "Faculty entry successfully deleted.";
                return RedirectToAction("Index", "Home");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editFaculty.FId });
        }

    }
}
