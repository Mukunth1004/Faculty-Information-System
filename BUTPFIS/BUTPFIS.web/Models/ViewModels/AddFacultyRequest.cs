using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BUTPFIS.web.Models.ViewModels
{
    public class AddFacultyRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FacultyImageUrl { get; set; }

        [Required]
        public string PersonalInfo { get; set; }

        [Required]
        [Url]
        public string GoogleScholarLink { get; set; }

        [Required]
        [Url]
        public string ResearchGateLink { get; set; }

        [Required]
        public string Expertise { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Honours { get; set; }

        [Required]
        public string Patents { get; set; }

        [Required]
        public string Publications { get; set; }

        [Required]
        public string Seminar { get; set; }

        //Display courses
        public IEnumerable<SelectListItem> Courses { get; set; }

        // Collect Tag
        [Required]
        public string[] SelectedCourses { get; set; } = Array.Empty<string>();
    }
}
