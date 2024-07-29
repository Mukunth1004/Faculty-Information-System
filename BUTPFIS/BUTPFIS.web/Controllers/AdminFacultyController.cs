using Azure;
using BUTPFIS.web.Data;
using BUTPFIS.web.Models.Domain;
using BUTPFIS.web.Models.ViewModels;
using BUTPFIS.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BUTPFIS.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminFacultyController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly IFacultyRepository facultyRepository;

        public AdminFacultyController(ICourseRepository courseRepository, IFacultyRepository facultyRepository)
        {
            this.courseRepository = courseRepository;
            this.facultyRepository = facultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get courses from repository
            var courses = await courseRepository.GetAllASync();

            var model = new AddFacultyRequest
            {
                Courses = courses.Select(x => new SelectListItem { Text = x.CourseName, Value = x.CourseId.ToString()})
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFacultyRequest addFacultyRequest)
        {
            var facultyInfo = new FacultyInfo
            {
                Name = addFacultyRequest.Name,
                Designation = addFacultyRequest.Designation,
                MobileNo = addFacultyRequest.MobileNo,
                Email = addFacultyRequest.Email,
                FacultyImageUrl = addFacultyRequest.FacultyImageUrl,
                PersonalInfo = addFacultyRequest.PersonalInfo,
                GoogleScholarLink = addFacultyRequest.GoogleScholarLink,
                ResearchGateLink = addFacultyRequest.ResearchGateLink,
                Expertise = addFacultyRequest.Expertise,
                Experience = addFacultyRequest.Experience,
                Education = addFacultyRequest.Education,
                Honours = addFacultyRequest.Honours,
                Patents = addFacultyRequest.Patents,
                Publications = addFacultyRequest.Publications,
                Seminar = addFacultyRequest.Seminar
            };

            // Map Courses from Selected Courses
            var selectedCourses = new List<CourseInfo>();
            foreach (var selectedCourseId in addFacultyRequest.SelectedCourses)
            {
                var selectedCourseIdAsGuid = Guid.Parse(selectedCourseId);
                var existingCourse = await courseRepository.GetAsync(selectedCourseIdAsGuid);

                if (existingCourse != null)
                {
                    selectedCourses.Add(existingCourse);
                }
            }

            // Mapping courses back to domain model
            facultyInfo.CourseInfos = selectedCourses;

            await facultyRepository.AddAsync(facultyInfo);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(string? searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;

            var facultyinfos = await facultyRepository.GetAllASync(searchQuery);

            return View(facultyinfos);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var facultyinfo = await facultyRepository.GetAsync(id);
            var coursesDomainModel = await courseRepository.GetAllASync();

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
                    Courses = coursesDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.CourseName,
                        Value = x.CourseId.ToString()
                    }),
                    SelectedCourses = facultyinfo.CourseInfos.Select(x => x.CourseId.ToString()).ToArray()
                };

                return View(model);
            }

            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFaculty editFaculty)
        {
            // map view model back to domain model
            var facultyDomainModel = new FacultyInfo
            {
                FId = editFaculty.FId,
                Name = editFaculty.Name,
                Designation = editFaculty.Designation,
                MobileNo = editFaculty.MobileNo,
                Email = editFaculty.Email,
                FacultyImageUrl= editFaculty.FacultyImageUrl,
                PersonalInfo = editFaculty.PersonalInfo,
                GoogleScholarLink = editFaculty.GoogleScholarLink,
                ResearchGateLink = editFaculty.ResearchGateLink,
                Expertise = editFaculty.Expertise,
                Experience = editFaculty.Experience,
                Education = editFaculty.Education,
                Honours = editFaculty.Honours,
                Patents = editFaculty.Patents,
                Publications = editFaculty.Publications,
                Seminar= editFaculty.Seminar
            };

            // Map courses to domain model

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

            var updatedFaculty  = await facultyRepository.UpdateAsync(facultyDomainModel);

            if (updatedFaculty != null)
            {
                // Show success notification
            }
            else
            {
                // Show error notification
            }

            return RedirectToAction("List", new { id = editFaculty.FId});
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(EditFaculty editFaculty)
        {
            var deletedFaculty = await facultyRepository.DeleteAsync(editFaculty.FId);

            if (deletedFaculty != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editFaculty.FId });
        }

        public async Task<IActionResult> GetFacultiesByCourse(string courseName)
        {
            var faculties = await facultyRepository.GetFacultiesByCourseAsync(courseName);
            return View(faculties);
        }

    }
}
