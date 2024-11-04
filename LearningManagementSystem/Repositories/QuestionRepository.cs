using LearningManagementSystem.DAL;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;

namespace LearningManagementSystem.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(LMSContext context) : base(context)
        {
        }
    }
}
