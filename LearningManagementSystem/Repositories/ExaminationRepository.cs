using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class ExaminationRepository : Repository<Examination>, IExaminationRepository
    {
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public ExaminationRepository(
            LMSContext context, 
            IMapper mapper,
            IUserContext userContext) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<bool> AddExamination(ExaminationRequestDto erd)
        {
            var exam = _mapper.Map<Examination>(erd);
            exam.IsApprove = false;
            exam.Status = "Chờ phê duyệt";
            exam.DateCreated = DateTime.Now;
            exam.CreateBy = await _userContext.GetFullName();

            try
            {
                await _context.Examinations.AddAsync(exam);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            var questions = new List<QuestionExam>();

            foreach(var item in erd.Questions)
            {
                var question = _mapper.Map<QuestionExam>(item);
                question.SubjectId = exam.SubjectId;

                try
                {
                    await _context.QuestionExams.AddAsync(question);
                    await _context.SaveChangesAsync();

                    await _context.ExaminationQuestions.AddAsync(new ExaminationQuestion
                    {
                        ExaminationId = exam.Id,
                        QuestionExamId = question.Id,
                    });
                }
                catch
                {
                    return false;
                }

                foreach(var itemAnswer in item.Answers)
                {
                    var answer = _mapper.Map<AnswerExam>(itemAnswer);
                    answer.QuestionExamId = question.Id;

                    try
                    {
                        await _context.AnswerExams.AddAsync(answer);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<PagedResult<Examination>> GetExamination(PaginationParams paginationParams, string? DepartmentId, 
            string? SubjectId, string? ExamName)
        {
            var query = _context.Set<Examination>().AsQueryable();

            if(DepartmentId != null && DepartmentId.ToString() != "")
            {
                query = query
                    .Include(e => e.Subject)
                    .Where(e => e.Subject.DepartmentId == DepartmentId)
                    .Select(e => new Examination
                    {
                        Id = e.Id,
                        FormExam = e.FormExam,
                        Name = e.Name,
                        Duration = e.Duration,
                        SubjectId = e.SubjectId,
                        IsApprove = e.IsApprove,
                        Status = e.Status,
                        DateCreated = e.DateCreated,
                        CreateBy = e.CreateBy,
                    });
            }
            if(SubjectId != null && SubjectId.ToString() != "")
            {
                query = query.Where(e => e.SubjectId == SubjectId);
            }
            if(ExamName != null && ExamName.ToString() != "")
            {
                query = query.Where(e => e.Name == ExamName);
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<Examination>(items, totalItems,
                paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
