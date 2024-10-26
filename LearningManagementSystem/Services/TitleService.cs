using AutoMapper;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;

namespace LearningManagementSystem.Services
{
    public class TitleService : ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IMapper _mapper;
        public TitleService(ITitleRepository titleRepository, IMapper mapper)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Title>> GetAll()
        {
            return await _titleRepository.GetAll();
        }

        public async Task<IEnumerable<Title>> GetBySubject(string id)
        {
            return await _titleRepository.GetBySubject(id);
        }
        public async Task<bool> InsertTitle(TitleRequestDto title)
        {
            return await _titleRepository.Add(_mapper.Map<Title>(title));
        }
    }
}
