using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using ArgumentException = LearningManagementSystem.Exceptions.ArgumentException;

namespace LearningManagementSystem.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public SubjectRepository(
            LMSContext context,
            IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Subject>> GetAllInclude()
        {
            return await _context.Set<Subject>()
                .AsQueryable()
                .Include(p => p.Department)
                .ToListAsync();
        }

        public async Task<Subject> GetSubjectById(string id)
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

        public async Task<IEnumerable<Subject>> GetSubjectByStudent(string userId)
        {
            var subjectIds = await _context.UserSubjects
                .Where(us => us.UserId == userId)
                .Select(us => us.SubjectId)
                .ToListAsync();

            return await _context.Subjects
                .Include(us => us.Department)
                .Where(us => subjectIds.Any(s => s == us.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectByTeacher(string userId)
        {
            return await _context.Subjects
                .Include(us => us.Department)
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> UpdateSubject(SubjectRequestDto subject, string subjectId)
        {
            if (subject == null || subject == null)
            {
                throw new ArgumentException("Yêu cầu nhập đầy đủ thông tin");
            }

            var exitsSubject = await GetSubjectById(subjectId);

            if (exitsSubject == null)
            {
                throw new NotFoundException("Không tìm thấy môn học");
            }

            exitsSubject.Name = subject.Name;
            exitsSubject.Note = subject.Note;
            exitsSubject.Description = subject.Description;
            exitsSubject.AcademicYearId = subject.AcademicYearId;
            exitsSubject.DateOfSubmitForApprove = subject.DateOfSubmitForApprove;
            _context.Subjects.Update(exitsSubject);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
