using LearningManagementSystem.DAL;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly LMSContext _context;
        public SubjectRepository(
            LMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllInclude()
        {
            return await _context.Set<Subject>()
                .AsQueryable()
                .Include(p => p.Teacher)
                .ToListAsync();
        }

        public async Task<Subject?> GetSubjectById(string id)
        {
            try
            {
                return await _context.Subjects
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
