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
        private readonly IDocumentService _documentService;
        private readonly IUserContext _userContext;
        public SubjectService(
            ISubjectRepository subjectRepository,
            LMSContext context,
            IMapper mapper,
            IDocumentService documentService,
            IUserContext userContext)
        {
            _subjectRepository = subjectRepository;
            _context = context;
            _mapper = mapper;
            _documentService = documentService;
            _userContext = userContext;        }
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
                var docBySubject = await _documentService.GetDocumentBySubject(item.Id);
                var docAwait = docBySubject.Where(x => !x.IsAprroved).ToList();
                int cntDocAwait = docAwait.Count;
                subject.Department = item.Department.Name;
                subject.DocAwaitAprrove = cntDocAwait;
                subjectsResponse.Add(subject);
            }
            return subjectsResponse;
        }

        public async Task<bool> UpdateSubject(Subject subject)
        {
            return await _subjectRepository.Update(subject);
        }

        public async Task<Subject> GetSubject(string id)
        {
            return await _subjectRepository.GetSubjectById(id);
        }

        public async Task<List<Subject>> GetSubjectsByUser()
        {
            var userId = await _userContext.GetId();

            var subjects = await _context.UserSubjects
                .Where(s => s.UserId == userId)
                .Select(s => s.SubjectId)
                .ToListAsync();

            return await _context.Subjects
               .Where(s => subjects.Any(p => p == s.Id))
               .ToListAsync();
        }
    }
}
