using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly LMSContext _context;
        public QuestionRepository(LMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsBySubject(int LessionId)
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
