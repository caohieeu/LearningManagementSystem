using LearningManagementSystem.DAL;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;

namespace LearningManagementSystem.Repositories
{
    public class LessionRepository : Repository<Lession>, ILessionRepository
    {
        public LessionRepository(LMSContext context) : base(context)
        {
        }
    }
}
