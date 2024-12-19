using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using Microsoft.EntityFrameworkCore;
using ArgumentException = LearningManagementSystem.Exceptions.ArgumentException;

namespace LearningManagementSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;
        private readonly ITitleService _titleService;
        private readonly ILessionService _lessionService;
        private readonly IUserService _userService;
        private readonly IUserContext _userContext;
        public SubjectService(
            ISubjectRepository subjectRepository,
            LMSContext context,
            IMapper mapper,
            IDocumentService documentService,
            IUserContext userContext,
            ITitleService titleService,
            ILessionService lessionService,
            IUserService userService)
        {
            _subjectRepository = subjectRepository;
            _context = context;
            _mapper = mapper;
            _documentService = documentService;
            _userContext = userContext;        
            _titleService = titleService;
            _lessionService = lessionService;
            _userService = userService;
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
            subject.StatusOfSubjectDoc = "Đang chờ";
            subject.Note = "Chưa có ghi chú thêm";
            try
            {
                return await _subjectRepository.Add(subject);
            }
            catch
            {
                throw new BadHttpRequestException("Thêm môn học không thành công");
            }
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
                var teacher = await _userService.GetUserById(item.UserId);
                subject.Department = item.Department.Name;
                subject.DocAwaitAprrove = cntDocAwait;
                subject.Teacher = teacher.FullName;

                subjectsResponse.Add(subject);
            }
            return subjectsResponse;
        }

        public async Task<Subject> GetSubject(string id)
        {
            return await _subjectRepository.GetSubjectById(id);
        }

        public async Task<List<SubjectResponseDto>> GetSubjectsByUser()
        {
            var currUser = await _userContext.GetCurrentUserInfo();

            var listSubject =
                currUser.Role == Utils.Roles.Student ?
                await _subjectRepository.GetSubjectByStudent(currUser.Id) :
                await _subjectRepository.GetSubjectByTeacher(currUser.Id);

            List <SubjectResponseDto> subjectsResponse = new List<SubjectResponseDto>();
            foreach (var item in listSubject)
            {
                SubjectResponseDto subject = _mapper.Map<SubjectResponseDto>(item);
                var docBySubject = await _documentService.GetDocumentBySubject(item.Id);
                var docAwait = docBySubject.Where(x => !x.IsAprroved).ToList();
                int cntDocAwait = docAwait.Count;
                var teacher = await _userService.GetUserById(item.UserId);
                subject.Department = item.Department.Name;
                subject.DocAwaitAprrove = cntDocAwait;
                subject.Teacher = teacher.FullName;

                subjectsResponse.Add(subject);
            }
            return subjectsResponse;
        }
        public async Task<Subject> GetSubjectById(string subjectId)
        {
            return await _subjectRepository.GetSubjectById(subjectId);
        }
        public async Task<bool> AssignSubjectToStudent(AssignSubjectDto subject)
        {
            if(subject == null)
            {
                throw new ArgumentException("Môn học không hợp lệ");
            }

            var userSubject = _mapper.Map<UserSubjects>(subject);
            _context.UserSubjects.Add(userSubject);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SubjectDetailDto> GetSubjectDetail(string subjectId, string classId)
        {
            var subjectDetail = new SubjectDetailDto();

            var subjectById = await GetSubjectById(subjectId);

            if(subjectById == null || subjectId == null)
            {
                throw new NotFoundException("Không tìm thấy môn học");
            }

            subjectDetail = _mapper.Map<SubjectDetailDto>(subjectById);
            subjectDetail.Titles = new List<TitleStudentDto>();

            var curUser = await _userContext.GetCurrentUserInfo();
            List<TitleStudentDto> titlesStudent = new List<TitleStudentDto>();

            var titles = await _titleService.GetBySubject(subjectId);
            foreach (var tit in titles)
            {
                var titleStudent = _mapper.Map<TitleStudentDto>(tit);
                var lession = await _lessionService.GetByTitleAndClass(tit.Id, classId);
                foreach(var file in lession.FileResponses)
                {
                    if(file.IsAprroved)
                    {
                        titleStudent.Lessions = lession;
                    }
                }

                subjectDetail.Titles.Add(titleStudent);
            }
            subjectDetail.Teacher = (from u in _context.Users
                                     where u.Id == subjectById.UserId
                                     select u.FullName).FirstOrDefault() ?? string.Empty;

            return subjectDetail;
        }

        public async Task<bool> UpdateSubject(SubjectRequestDto subject, string subjectId)
        {
            return await _subjectRepository.UpdateSubject(subject, subjectId);
        }
    }
}
