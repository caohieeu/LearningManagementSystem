using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LearningManagementSystem.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public DocumentRepository(LMSContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Document>> FindDocumentByName(string name)
        {
            return await _context.Documents
                .Where(x => x.FileName.Contains(name))
                .ToListAsync();
        }

        public Task<List<Document>> GetDocumentBySubject(string subjectId)
        {
            return GetDocumentBySubject(subjectId, _context);
        }

        public async Task<List<Document>> GetDocumentBySubject(string subjectId, LMSContext _context)
        {
            return await _context.DocumentLessions
                .Include(x => x.Document)
                .Include(x => x.Lession.Title.Subject)
                .Where(x => x.Lession.Title.Subject.Id == subjectId)
                .GroupBy(x => x.DocumentId)
                .Select(g => g.FirstOrDefault().Document)
                .ToListAsync();
        }
    }
}
