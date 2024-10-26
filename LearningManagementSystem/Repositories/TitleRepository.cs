using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class TitleRepository : Repository<Title>, ITitleRepository
    {
        private readonly LMSContext _context;
        public TitleRepository(LMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddTitle(TitleRequestDto title)
        {
            return true;
        }

        public Task<List<Title>> GetBySubject(string id)
        {
            try
            {
                return _context.Titles
                .Where(x => x.SubjectId == id)
                .ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
