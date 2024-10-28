using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace LearningManagementSystem.Repositories
{
    public class TitleRepository : Repository<Title>, ITitleRepository
    {
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public TitleRepository(LMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddTitle(TitleRequestDto title)
        {
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            try
            {
                var title = await _context.Titles
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                if(title == null)
                {
                    throw new NotFoundException("Không tìm thấy chủ đề");
                }

                _context.Titles.Remove(title);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
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
