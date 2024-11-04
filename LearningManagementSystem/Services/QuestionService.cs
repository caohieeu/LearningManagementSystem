using AutoMapper;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;

namespace LearningManagementSystem.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<bool> InsertQuestion(QuestionRequestDto question)
        {
            return await _questionRepository.Add(_mapper.Map<Question>(question));
        }
    }
}
