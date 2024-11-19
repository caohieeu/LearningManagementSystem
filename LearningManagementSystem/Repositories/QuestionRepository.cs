using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly LMSContext _context;
        private readonly IUserContext _userContext;
        public QuestionRepository(
            LMSContext context,
            IUserContext userContext) : base(context)
        {
            _context = context;
            _userContext = userContext;
        }
        public async Task<PagedResult<Question>> GetQuestionByFilter(FilterQuestionDto filter, PaginationParams paginationParams)
        {
            List<Question> questions = new List<Question>();
            var query = _context.Set<Question>().AsQueryable();

            if(filter.LessionId != 0 && filter.LessionId != null)
            {
                query = query.Where(q => q.LessionId == filter.LessionId);
            }
            if(filter.TypeQuestion != null && filter.TypeQuestion.ToString() != "")
            {
                if(filter.TypeQuestion.ToString() == "gannhat")
                {
                    query = query.OrderByDescending(q => q.CreatedAt);
                }
                else if (filter.TypeQuestion.ToString() == "chuatraloi")
                {
                    query = query.Where(q => !_context.Answers.Any(a => a.QuestionId == q.Id));
                }
                else if (filter.TypeQuestion.ToString() == "datraloi")
                {
                    query = query.Where(q => _context.Answers.Any(a => a.QuestionId == q.Id));
                }
            }
            if(filter.FillQuestion != null && filter.FillQuestion.ToString() != "")
            {
                if(filter.FillQuestion.ToString() == "toihoi")
                {
                    string userId = await _userContext.GetId();
                    query = query.Where(q => q.UserId == userId);
                }
                if(filter.FillQuestion.ToString() == "toithich")
                {
                    query = query.Where(q =>
                        _context.Favorites.Any(f => 
                            f.ItemId == q.Id && 
                            f.ItemType == TypeQA.question));
                }
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<Question>(items.ToList(), totalItems,
                paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<List<Question>> GetQuestionsByLession(int LessionId)
        {
            return await _context.Questions
                .Where(x => x.LessionId == LessionId)
                .ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByTitleName(string title)
        {
            return await _context.Questions
                .Where(x => x.Title.Contains(title))
                .ToListAsync();
        }
    }
}
