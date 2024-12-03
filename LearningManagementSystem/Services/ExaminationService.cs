using AutoMapper;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using OfficeOpenXml;

namespace LearningManagementSystem.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly IMapper _mapper;
        public ExaminationService(IExaminationRepository examinationRepository, IMapper mapper)
        {
            _examinationRepository = examinationRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> AddExam(ExaminationRequestDto exam)
        {
            return await _examinationRepository.AddExamination(exam);
        }

        public async Task<bool> AddFromQuestionBank(ExQuestionBankRequestDto exam)
        {
            return await _examinationRepository.AddFromQuestionBank(exam);
        }

        public async Task<bool> DeleteExam(int id)
        {
            var exam = await _examinationRepository.GetById(id);

            if (exam == null)
            {
                throw new NotFoundException("Không tìm thấy bài thi");
            }

            return await _examinationRepository.Remove(exam);
        }

        public async Task<PagedResult<ExaminationResponseDto>> GetExamination(PaginationParams paginationParams, string? DepartmentId,
            string? SubjectId, string? ExamName)
        {
            var listExam = await _examinationRepository.GetExamination(paginationParams, DepartmentId, 
                SubjectId, ExamName);

            var lisResult = new PagedResult<ExaminationResponseDto>(new List<ExaminationResponseDto>(), listExam.TotalItems,
                listExam.PageNumber, listExam.PageSize);

            lisResult.Items = listExam.Items
                .Select(e => _mapper.Map<ExaminationResponseDto>(e))
                .ToList();

            return lisResult;
        }

        public async Task<PagedResult<QuestionBankResponseDto>> GetQuestionByFilter(FilterQExaminationDto filter, PaginationParams paginationParams)
        {
            var questions = await _examinationRepository.GetQuestionByFilter(filter, paginationParams);
            var result = new PagedResult<QuestionBankResponseDto>(new List<QuestionBankResponseDto>(),
                questions.TotalItems, questions.PageNumber, questions.PageSize);
            foreach(var question in questions.Items)
            {
                result.Items.Add(_mapper.Map<QuestionBankResponseDto>(question));
            }

            return result;
        }

        public async Task<List<QAExamResponseDto>> GetInformationExamination(int id)
        {
            var listQA = new List<QAExamResponseDto>();
            var listQuestion = await _examinationRepository.GetQuestionExamination(id);
            
            foreach (var item in listQuestion)
            {
                var qa = _mapper.Map<QAExamResponseDto>(item);
                var answers = await _examinationRepository.GetAnswerExamination(item.Id);
                qa.answerExams = answers
                    .Select(a => _mapper.Map<AnswerExamResponseDto>(a))
                    .ToList();

                listQA.Add(qa);
            }

            return listQA;
        }

        public Task<bool> AddQuestionBank(QuestionBankRequestDto questionAxamRequestDto)
        {
            return _examinationRepository.AddQuestionBank(questionAxamRequestDto);
        }

        public Task<bool> DeleteQuestionBank(int questionId)
        {
            return _examinationRepository.DeleteQuestionBank(questionId);
        }
        public Task<bool> UpdateQuestionBank(QuestionBankRequestDto questionAxamRequestDto, int questionId)
        {
            return _examinationRepository.UpdateQuestionBank(questionAxamRequestDto, questionId);
        }

        public Task<bool> ApproveExamination(int examId)
        {
            return _examinationRepository.ApproveExamination(examId);
        }

        public Task<bool> CancelApproveExamination(int examId)
        {
            return _examinationRepository.CancelApproveExamination(examId);
        }

        public async Task<ExaminationResponseDto> GetDetailExamination(int examId)
        {
            var examination = await _examinationRepository.GetDetailExamination(examId);

            var examinationMapper = _mapper.Map<ExaminationResponseDto>(examination);

            return examinationMapper;
        }
    }
}
