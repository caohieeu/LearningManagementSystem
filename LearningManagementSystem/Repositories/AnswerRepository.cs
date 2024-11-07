using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private readonly LMSContext _context;
        public AnswerRepository(LMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetAnswerByQuestion(int questionId)
        {
            return await _context.Answers
                .Where(x => x.QuestionId == questionId)
                .ToListAsync();
        }
    }
}
