namespace StudentManagerApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Credits { get; set; }

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
