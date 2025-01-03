﻿using AutoMapper;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Services
{
    public class TitleService : ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IMapper _mapper;
        public TitleService(
            ITitleRepository titleRepository, 
            IMapper mapper)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Title>> GetAll()
        {
            return await _titleRepository.GetAll();
        }

        public async Task<IEnumerable<TitleResponseDto>> GetBySubject(string subjectId)
        {
            var listTitle = await _titleRepository.GetBySubject(subjectId);
            return listTitle.Select(title => _mapper.Map<TitleResponseDto>(title));
        }
        public async Task<bool> InsertTitle(TitleRequestDto title)
        {
            if(!await _titleRepository.Add(_mapper.Map<Title>(title)))
            {
                throw new NotFoundException("Không tìm thấy subject id");
            }
            return true;
        }
        public async Task<bool> UpdateTitle(TitleRequestDto title, int id)
        {
            var titleUpdate = _titleRepository.GetById(id);
            if (titleUpdate == null)
            {
                throw new NotFoundException("Không tìm thấy chủ đề");
            }
            await _titleRepository.Update(_mapper.Map<Title>(titleUpdate));
            return true;
        }
        public async Task<bool> RemoveTitle(int id)
        {
            return await _titleRepository.DeleteById(id);
        }
    }
}
