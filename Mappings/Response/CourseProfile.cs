using AutoMapper;
using StudentManagerApp.DTOs.Response;
using StudentManagerApp.Models;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseBaseResponse>();
    }
}
