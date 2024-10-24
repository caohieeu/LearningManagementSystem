using AutoMapper;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Mapper
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
        {
            CreateMap<SubjectRequestDto, Subject>().ReverseMap();
            CreateMap<SubjectResponseDto, Subject>().ReverseMap();
            CreateMap<UserResponseDto, ApplicationUser>().ReverseMap();
        }
    }
}
