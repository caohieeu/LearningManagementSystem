using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;

namespace LearningManagementSystem.Repositories
{
    public class ExaminationRepository : Repository<Examination>, IExaminationRepository
    {
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IExcelExportService _excelExportService;
        public ExaminationRepository(
            LMSContext context, 
            IMapper mapper,
            IUserContext userContext,
            IExcelExportService excelExportService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
            _excelExportService = excelExportService;
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

            var listQuestion = new List<QuestionExam>();

            foreach(var item in erd.Questions)
            {
                var question = _mapper.Map<QuestionExam>(item);
                question.SubjectId = exam.SubjectId;
                question.CreatedDate = DateTime.Now;
                question.LastUpdate = DateTime.Now;
                question.CreatedBy = await _userContext.GetFullName();
                question.UpdatedBy = await _userContext.GetFullName();

                try
                {
                    await _context.QuestionExams.AddAsync(question);
                    await _context.SaveChangesAsync();
                    listQuestion.Add(question);

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

                if(question.Type == QEType.TracNghiem)
                {
                    foreach (var itemAnswer in item.Answers)
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
            }

            await ExportQuestionToExcel(listQuestion, erd.Name);

            return true;
        }
        async Task ExportQuestionToExcel(List<QuestionExam> questions, string examName)
        {
            var filecontent = await _excelExportService.ExportQuestionsToExcel(questions, examName);

            var filePath = Path.Combine("wwwroot/exports", $"{examName}_questions.xlsx");
            File.WriteAllBytes(filePath, filecontent);
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
                query = query.Where(e => e.Name.Contains(ExamName));
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<Examination>(items, totalItems,
                paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<List<QuestionExam>> GetQuestionExamination(int examId)
        {
            return await _context.ExaminationQuestions
                .Include(e => e.QuestionExam)
                .Where(e => e.ExaminationId == examId)
                .Select(e => new QuestionExam
                {
                    Id = e.QuestionExam.Id,
                    Type = e.QuestionExam.Type,
                    QuestionText = e.QuestionExam.QuestionText,
                    Order = e.QuestionExam.Order,
                    Level = e.QuestionExam.Level,
                    SubjectId = e.QuestionExam.SubjectId,
                }).ToListAsync();
        }
        public async Task<List<AnswerExam>> GetAnswerExamination(int questionId)
        {
            return await _context.AnswerExams
                .Where(q => q.QuestionExamId == questionId)
                .ToListAsync();
        }

        public async Task<bool> AddFromQuestionBank(ExQuestionBankRequestDto exam)
        {
            for(int i = 0; i < exam.Quantity; i++)
            {
                var examination = _mapper.Map<Examination>(exam);
                examination.Name = $"{exam.Name}_{i}";
                examination.SubjectId = exam.SubjectId;
                examination.IsApprove = false;
                examination.Status = "Chờ phê duyệt";
                examination.DateCreated = DateTime.Now;
                examination.CreateBy = await _userContext.GetFullName();
                examination.FormExam = "Trắc nghiệm";

                _context.Examinations.Add(examination);
                _context.SaveChanges();

                var usedQuestion = new List<QuestionExam>();

                for (int j = 0; j < exam.CntQuestionEasy; j++)
                {
                    var random = new Random();

                    var cnt = _context.QuestionExams
                        .Where(q => q.Level == LevelType.Easy &&
                            !usedQuestion.Select(uq => uq.Id).Contains(q.Id) &&
                            q.Type == QEType.TracNghiem &&
                            q.SubjectId==exam.SubjectId)
                        .Count();
                    var question = await _context.QuestionExams
                        .Where(q => q.Level == LevelType.Easy &&
                            q.SubjectId==exam.SubjectId &&
                            q.Type == QEType.TracNghiem &&
                            !usedQuestion.Select(uq => uq.Id).Contains(q.Id))
                        .Skip(random.Next(0, cnt))
                        .Take(1)
                        .SingleAsync();
                    usedQuestion.Add(question);

                    _context.ExaminationQuestions.Add(new ExaminationQuestion
                    {
                        ExaminationId = examination.Id,
                        QuestionExamId = question.Id
                    });
                }
                for (int j = 0; j < exam.CntQuestionMedium; j++)
                  {
                    var random = new Random();

                    var cnt = _context.QuestionExams
                        .Where(q => q.Level == LevelType.Medium &&
                            !usedQuestion.Select(uq => uq.Id).Contains(q.Id) &&
                            q.Type == QEType.TracNghiem &&
                            q.SubjectId == exam.SubjectId)
                        .Count();

                    var question = await _context.QuestionExams
                        .Where(q => q.Level == LevelType.Medium &&
                            q.SubjectId==exam.SubjectId &&
                            q.Type == QEType.TracNghiem &&
                            !usedQuestion.Select(uq => uq.Id).Contains(q.Id))
                        .Skip(random.Next(0, cnt))
                        .Take(1)
                        .SingleAsync();
                    usedQuestion.Add(question);

                    _context.ExaminationQuestions.Add(new ExaminationQuestion
                    {
                        ExaminationId = examination.Id,
                        QuestionExamId = question.Id
                    });
                }
                for (int j = 0; j < exam.CntQuestionHard; j++)
                {
                    var random = new Random();

                    var cnt = _context.QuestionExams
                        .Where(q => q.Level == LevelType.Hard &&
                            !usedQuestion.Select(uq => uq.Id).Contains(q.Id) &&
                            q.Type == QEType.TracNghiem &&
                            q.SubjectId == exam.SubjectId)
                        .Count();

                    var question = await _context.QuestionExams
                        .Where(q => q.Level == LevelType.Hard &&
                            q.SubjectId==exam.SubjectId &&
                            q.Type == QEType.TracNghiem &&
                            !usedQuestion.Select(uq => uq.Id).Contains(q.Id))
                        .Skip(random.Next(0, cnt))
                        .Take(1)
                        .SingleAsync();
                    usedQuestion.Add(question);

                    _context.ExaminationQuestions.Add(new ExaminationQuestion
                    {
                        ExaminationId = examination.Id,
                        QuestionExamId = question.Id
                    });
                }

                //examination.FileData = 
                //    await _excelExportService.ExportQuestionsToExcel(usedQuestion, examination.Name);
                //await _context.SaveChangesAsync();

                await ExportQuestionToExcel(usedQuestion, $"{exam.Name}_{i}");
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PagedResult<QuestionExam>> GetQuestionByFilter(FilterQExaminationDto filter, 
            PaginationParams paginationParams)
        {
            List<QuestionExam> questions = new List<QuestionExam>();
            var query = _context.Set<QuestionExam>().AsQueryable();

            if (filter.DepartmentId != null)
            {
                var listSubject = _context.Subjects
                    .Where(s => s.DepartmentId == filter.DepartmentId)
                    .ToList();

                query = query.Where(q => listSubject.Select(s => s.Id).Contains(q.SubjectId));
            }
            if (filter.SubjectId != null && filter.SubjectId.ToString() != "")
            {
                query = query.Where(q => q.SubjectId == filter.SubjectId);
            }
            if (filter.Levels != null && filter.Levels.Count() > 0)
            {
                foreach (var level in filter.Levels)
                {
                    query = query.Where(q => q.Level == level);
                }
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<QuestionExam>(items.ToList(), totalItems,
                paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<bool> AddQuestionBank(QuestionBankRequestDto questionAxamRequestDto)
        {
            try
            {
                var question = _mapper.Map<QuestionExam>(questionAxamRequestDto);
                question.CreatedDate = DateTime.Now;
                question.CreatedBy = await _userContext.GetFullName();
                question.UpdatedBy = await _userContext.GetFullName();
                question.LastUpdate = DateTime.Now;
                question.SubjectId = questionAxamRequestDto.SubjectId;

                await _context.QuestionExams.AddAsync(question);
                await _context.SaveChangesAsync();

                foreach (var answer in questionAxamRequestDto.Answers)
                {
                    var answerUpdate = _mapper.Map<AnswerExam>(answer);
                    answerUpdate.QuestionExamId = question.Id;

                    await _context.AnswerExams.AddAsync(answerUpdate);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteQuestionBank(int questionId)
        {
            var question = _context.QuestionExams
                .Where(q => q.Id == questionId)
                .FirstOrDefault();  

            if (question == null)
            {
                throw new NotFoundException("Không tìm thấy câu hỏi");
            }

            try
            {
                _context.QuestionExams.Remove(question);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return true;
            }

        }

        public async Task<bool> UpdateQuestionBank(QuestionBankRequestDto questionAxamRequestDto, int questionId)
        {
            try
            {
                var question = _context.QuestionExams
                    .Where(q => q.Id == questionId)
                    .FirstOrDefault();

                if (question == null)
                {
                    throw new NotFoundException("Không tìm thấy câu hỏi");
                }

                question.QuestionText = questionAxamRequestDto.QuestionText;
                question.Order = questionAxamRequestDto.Order;
                question.SubjectId = questionAxamRequestDto.SubjectId;
                question.Level = questionAxamRequestDto.Level;
                question.LastUpdate = DateTime.Now;
                question.UpdatedBy = await _userContext.GetFullName();

                _context.QuestionExams.Update(question);

                foreach(var answer in questionAxamRequestDto.Answers)
                {
                    var answerDelete = _context.AnswerExams
                        .Where(a => a.QuestionExamId == questionId)
                        .ToList();

                    _context.AnswerExams.RemoveRange(answerDelete);
                    
                    var answerUpdate = _mapper.Map<AnswerExam>(answer);
                    answerUpdate.QuestionExamId = questionId;

                    _context.AnswerExams.Add(answerUpdate);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
