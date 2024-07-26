using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BUTPFIS.web.Models.ViewModels
{
    public class AddCourseRequest
    {
        [Required]
        public string CourseName { get; set; }
    }
}
