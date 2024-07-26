using System.ComponentModel.DataAnnotations;

namespace BUTPFIS.web.Models.Domain
{
    public class CourseInfo
    {
        [Key]
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public ICollection<FacultyInfo> FacultyInfos { get; set; }
    }
}
