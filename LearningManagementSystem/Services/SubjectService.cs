using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public SubjectService(
            ISubjectRepository subjectRepository,
            LMSContext context,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> GenerateNewId()
        {
            var entity = await _context.Subjects
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            string newId;

            if (entity == null)
            {
                newId = "#DKL1";
            }
            else
            {
                var currentId = entity.Id;
                var idNumber = int.Parse(currentId.Substring(4));

                newId = $"#KDL{idNumber + 1}";
            }
            return newId;
        }

        public async Task<bool> AddSubject(SubjectRequestDto subjectDto)
        {
            Subject subject = _mapper.Map<Subject>(subjectDto);
            subject.Id = await GenerateNewId();
            return await _subjectRepository.Add(subject);
        }

        public async Task<bool> DeleteSubject(Subject subject)
        {
            return await _subjectRepository.Remove(subject);
        }

        public async Task<IEnumerable<SubjectResponseDto>> GetAllSubject()
        {
            var listSubject = await _subjectRepository.GetAllInclude();
            List<SubjectResponseDto> subjectsResponse = new List<SubjectResponseDto>();
            foreach(var item in listSubject)
            {
                SubjectResponseDto subject = _mapper.Map<SubjectResponseDto>(item);
                subject.Teacher = item.Teacher.FullName;
                subjectsResponse.Add(subject);
            }
            return subjectsResponse;
        }

        public async Task<bool> UpdateSubject(Subject subject)
        {
            return await _subjectRepository.Update(subject);
        }
    }
}
