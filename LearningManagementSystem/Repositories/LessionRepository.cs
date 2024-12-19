using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class LessionRepository : Repository<Lession>, ILessionRepository
    {
        private readonly LMSContext _context;
        public LessionRepository(LMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LessionResponseDto> GetByTitleAndClass(int titleId, string classId)
        {
            LessionResponseDto lession = new LessionResponseDto();

            //get title
            lession.Title = await _context.Lessions
                .Where(x => x.ClassId == classId && x.TitleId == titleId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync() ?? "";

            //get list file
            lession.FileResponses = _context.DocumentLessions
                .Include(x => x.Lession)
                .Include(x => x.Document)
                .Where(x => x.Lession.TitleId == titleId && x.Lession.ClassId == classId)
                .Select(x => new FileResponseDto
                {
                    FileName = x.Document.FileName,
                    FileData = x.Document.FileData,
                    FileType = x.Document.FileType,
                    Type = x.Document.Type,
                    IsAprroved = x.Document.IsAprroved,
                })
                .ToList();

            return lession;
        }

        public Task<LessionResponseDto> GetLessionsByTitleAndClass(int titleId, string classId)
        {
            throw new NotImplementedException();
        }
    }
}
