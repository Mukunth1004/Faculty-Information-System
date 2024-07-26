using Microsoft.AspNetCore.Mvc.Rendering;

namespace BUTPFIS.web.Models.ViewModels
{
    public class EditFaculty
    {
        public Guid FId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string FacultyImageUrl { get; set; }
        public string PersonalInfo { get; set; }
        public string Expertise { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Honours { get; set; }
        public string Patents { get; set; }
        public string Publications { get; set; }
        public string Seminar { get; set; }

        //Display courses
        public IEnumerable<SelectListItem> Courses { get; set; }
        // Collect Tag
        public string[] SelectedCourses { get; set; } = Array.Empty<string>();
    }
}
