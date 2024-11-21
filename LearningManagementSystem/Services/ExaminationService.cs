using AutoMapper;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils.Pagination;

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
    }
}
