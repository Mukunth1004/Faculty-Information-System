using BUTPFIS.web.Data;
using BUTPFIS.web.Models.Domain;
using BUTPFIS.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BUTPFIS.web.Controllers
{
    public class AdminFacultyController : Controller
    {
        private readonly FISDbContext fisDbContext;

        public AdminFacultyController(FISDbContext fisDbContext)
        {
            this.fisDbContext = fisDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
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
                PersonalInfo = addFacultyRequest.PersonalInfo,
                Expertise = addFacultyRequest.Expertise,
                Experience = addFacultyRequest.Experience,
                Education = addFacultyRequest.Education,
                Honours = addFacultyRequest.Honours,
                Patents = addFacultyRequest.Patents,
                Publications = addFacultyRequest.Publications
            };

            await fisDbContext.FacultyInfos.AddAsync(facultyInfo);
            await fisDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var facultyinfos = await fisDbContext.FacultyInfos.ToListAsync();

            return View(facultyinfos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var facultyinfo = await fisDbContext.FacultyInfos.FirstOrDefaultAsync(x => x.FId == id);

            if (facultyinfo != null)
            {
                var editFaculty = new EditFaculty
                {
                    FId = facultyinfo.FId,
                    Name = facultyinfo.Name,
                    Designation = facultyinfo.Designation,
                    MobileNo = facultyinfo.MobileNo,
                    Email = facultyinfo.Email,
                    PersonalInfo = facultyinfo.PersonalInfo,
                    Expertise = facultyinfo.Expertise,
                    Experience = facultyinfo.Experience,
                    Education = facultyinfo.Education,
                    Honours = facultyinfo.Honours,
                    Patents = facultyinfo.Patents,
                    Publications = facultyinfo.Publications
                };

                return View(editFaculty);
            }

            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFaculty editFaculty)
        {
            // map view model back to domain model
            var editFacultyInfo = new FacultyInfo
            {
                FId = editFaculty.FId,
                Name = editFaculty.Name,
                Designation = editFaculty.Designation,
                MobileNo = editFaculty.MobileNo,
                Email = editFaculty.Email,
                PersonalInfo = editFaculty.PersonalInfo,
                Expertise = editFaculty.Expertise,
                Experience = editFaculty.Experience,
                Education = editFaculty.Education,
                Honours = editFaculty.Honours,
                Patents = editFaculty.Patents,
                Publications = editFaculty.Publications
            };

            var existingFaculty = await fisDbContext.FacultyInfos.FindAsync(editFacultyInfo.FId);

            if (existingFaculty != null)
            {
                existingFaculty.Name = editFacultyInfo.Name;
                existingFaculty.Designation = editFacultyInfo.Designation;
                existingFaculty.MobileNo = editFacultyInfo.MobileNo;
                existingFaculty.MobileNo = editFacultyInfo.MobileNo;
                existingFaculty.Email = editFacultyInfo.Email;
                existingFaculty.PersonalInfo = editFacultyInfo.PersonalInfo;
                existingFaculty.Expertise = editFacultyInfo.Expertise;
                existingFaculty.Experience = editFacultyInfo.Experience;
                existingFaculty.Education = editFacultyInfo.Education;
                existingFaculty.Honours = editFacultyInfo.Honours;
                existingFaculty.Patents = editFacultyInfo.Patents;
                existingFaculty.Publications = editFacultyInfo.Publications;

                await fisDbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editFacultyInfo.FId});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditFaculty editFaculty)
        {
            var deletedFaculty = await fisDbContext.FacultyInfos.FindAsync(editFaculty.FId);

            if (deletedFaculty != null)
            {
                fisDbContext.FacultyInfos.Remove(deletedFaculty);
                await fisDbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editFaculty.FId });
        }
    }
}
