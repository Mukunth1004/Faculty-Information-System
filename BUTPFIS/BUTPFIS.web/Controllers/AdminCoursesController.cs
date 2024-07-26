using BUTPFIS.web.Data;
using BUTPFIS.web.Models.Domain;
using BUTPFIS.web.Models.ViewModels;
using BUTPFIS.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BUTPFIS.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCourseController : Controller
    {
        private readonly ICourseRepository courseRepository;

        public AdminCourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseRequest addCourseRequest)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }


            var courseInfo = new CourseInfo
            {
                CourseName = addCourseRequest.CourseName
            };

            await courseRepository.AddAsync(courseInfo);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(string? searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;  

            var courseinfos = await courseRepository.GetAllASync(searchQuery);

            return View(courseinfos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var courseinfo = await courseRepository.GetAsync(id);

            if (courseinfo != null)
            {
                var editCourse = new EditCourse
                {
                    CourseId = courseinfo.CourseId,
                    CourseName = courseinfo.CourseName
                };

                return View(editCourse);
            }

            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourse editCourse)
        {
            // map view model back to domain model
            var editCourseInfo = new CourseInfo
            {
                CourseId = editCourse.CourseId,
                CourseName = editCourse.CourseName
            };

            var updatedCourse = await courseRepository.UpdateAsync(editCourseInfo);

            if (updatedCourse != null)
            {
                // Show success notification
            }
            else
            {
                // Show error notification
            }

            return RedirectToAction("List", new { id = editCourse.CourseId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditCourse editCourse)
        {
            var deletedCourse = await courseRepository.DeleteAsync(editCourse.CourseId);

            if (deletedCourse != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editCourse.CourseId });
        }
    }
}
