﻿using AutoMapper;
using LearningManagementSystem.Dtos;
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
            CreateMap<TitleResponseDto, Title>().ReverseMap();
            CreateMap<TitleRequestDto, Title>().ReverseMap();
            CreateMap<LessionDto, Lession>().ReverseMap();
            CreateMap<QuestionRequestDto, Question>().ReverseMap();
            CreateMap<AnswerRequestDto, Answer>().ReverseMap();
            CreateMap<AnswerResponseDto, Answer>().ReverseMap();
            CreateMap<QuestionResponseDto, Question>().ReverseMap();
        }
    }
}
