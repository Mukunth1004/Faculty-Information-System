using BUTPFIS.web.Models;
using BUTPFIS.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BUTPFIS.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFacultyRepository facultyRepository;

        public HomeController(ILogger<HomeController> logger, IFacultyRepository facultyRepository)
        {
            _logger = logger;
            this.facultyRepository = facultyRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MscSpecializations()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FacultiesByCourse(string courseName)
        {
            var faculties = await facultyRepository.GetFacultiesByCourseAsync(courseName);
            ViewData["CourseName"] = courseName;
            return View("FacultiesByCourse", faculties);
        }

    }
}
