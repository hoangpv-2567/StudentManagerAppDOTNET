using AutoMapper;
using StudentManagerApp.DTOs.Response;
using StudentManagerApp.Models;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDetailResponse>()
          .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));

        CreateMap<Student, StudentBaseResponse>();
    }
}
