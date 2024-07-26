namespace StudentManagerApp.DTOs.Response {
  public class StudentDetailResponse : StudentBaseResponse {
    public List<CourseBaseResponse> Courses { get; set; }
  }
}
