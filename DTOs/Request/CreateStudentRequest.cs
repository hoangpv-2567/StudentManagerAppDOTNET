using System.ComponentModel.DataAnnotations;

namespace StudentManagerApp.DTOs.Request {
    public class CreateStudentRequest {
        [Required]
        public string Name { get; set; }
        [Required]
        public string EnrollmentDate { get; set; }
        public List<int> SelectedCourseIds { get; set; }
    }
}
