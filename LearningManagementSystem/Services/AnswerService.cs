using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using System.Diagnostics.CodeAnalysis;

namespace LearningManagementSystem.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IFavoriteService _favoriteService;
        private readonly IAccountService _accountService;
        public AnswerService(
            IAnswerRepository answerRepository,
            IMapper mapper,
            IUserContext userContext,
            IFavoriteService favoriteService,
            IAccountService accountService)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _userContext = userContext;
            _favoriteService = favoriteService;
            _accountService = accountService;
        }

        public async Task<bool> DeleteAnswer(int id)
        {
            var answerExist = await _answerRepository.GetById(id);

            if (answerExist == null)
            {
                throw new NotFoundException("Không tìm thấy câu trả lời");
            }

            return await _answerRepository.Remove(answerExist);
        }

        public async Task<List<AnswerResponseDto>> GetAnswerByQuestion(int questionId)
        {
            var answers = await _answerRepository.GetAnswerByQuestion(questionId);

            if(answers.Count <= 0)
            {
                throw new NotFoundException("Không tìm thấy câu trả lời");
            }

            var response = new List<AnswerResponseDto>();

            foreach(var answer in answers)
            {
                var answerResponse = _mapper.Map<AnswerResponseDto>(answer);
                answerResponse.FavoriteQuantity = 
                    await _favoriteService.GetFavoriteByItem(answerResponse.Id, Utils.TypeQA.answer);
                answerResponse.User = await _accountService.GetUserById(answer.UserId);

                response.Add(answerResponse);
            }   

            return response;
        }

        public async Task<bool> InsertAnswer(AnswerRequestDto answer)
        {
            var answerReq = _mapper.Map<Answer>(answer);
            answerReq.IsShow = true;
            answerReq.UserId = await _userContext.GetId();
            answerReq.QuestionId = answer.QuestionId;
            answerReq.DateTime = DateTime.Now;  

            try
            {
                return await _answerRepository.Add(answerReq);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAnswer(AnswerRequestDto answer, int id)
        {
            var answerExist = await _answerRepository.GetById(id);

            if(answerExist == null)
            {
                throw new NotFoundException("Không tìm thấy câu trả lời");
            }

            answerExist.Name = answer.Name;
            answerExist.Content = answer.Content;
            answerExist.IsShow = answer.IsShow;
            answerExist.DateTime = DateTime.Now;

            return await _answerRepository.Update(answerExist);
        }
    }
}
