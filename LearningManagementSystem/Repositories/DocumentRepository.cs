using LearningManagementSystem.DAL;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;

namespace LearningManagementSystem.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(LMSContext context) : base(context)
        {
        }
    }
}
