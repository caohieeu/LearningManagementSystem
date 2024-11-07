using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace LearningManagementSystem.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IFavoriteService _favoriteService;
        public QuestionService(
            IQuestionRepository questionRepository,
            IMapper mapper,
            IUserContext userContext,
            IFavoriteService favoriteService)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _userContext = userContext;
            _favoriteService = favoriteService;
        }

        public async Task<List<QuestionResponseDto>> GetQuestionsBySubject(int LessionId)
        {   
            var questions = await _questionRepository.GetQuestionsBySubject(LessionId);
            var result = new List<QuestionResponseDto>();
            foreach (var question in questions)
            {
                var item = _mapper.Map<QuestionResponseDto>(question);
                item.Favorites = await _favoriteService.GetFavoriteByItem(item.Id, Utils.TypeQA.question);

                result.Add(item);
            }

            return result;
        }

        public async Task<List<QuestionResponseDto>> GetQuestionsByTitleName(string title)
        {
            var questions = await _questionRepository.GetQuestionsByTitleName(title);
            return questions
                .Select(question => _mapper.Map<QuestionResponseDto>(question))
                .ToList();
        }

        public async Task<bool> InsertQuestion(QuestionRequestDto question)
        {
            var quest = _mapper.Map<Question>(question);
            var user = await _userContext.GetCurrentUserInfo();

            quest.UserId = user.Id;
            return await _questionRepository.Add(quest);
        }
    }
}
