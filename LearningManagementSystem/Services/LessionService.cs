﻿using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Services
{
    public class LessionService : ILessionService
    {
        private readonly ILessionRepository _lessionRepository;
        private readonly LMSContext _context;
        private readonly IDocumentService _documentService;
        private readonly IAccountService _accountService;
        public LessionService(
            ILessionRepository lessionRepository,
            LMSContext context,
            IDocumentService documentService,
            IAccountService accountService)
        {
            _lessionRepository = lessionRepository;
            _context = context;
            _documentService = documentService;
            _accountService = accountService;
        }
        public Lession SaveLession(Lession lession)
        {
            _context.SaveChanges();
            return lession;
        }
        public async Task<bool> InsertLession(LessionRequestDto lessionRequest, 
            IFormFile fileData, FileType fileType)  
        {
            try
            {
                //var user = _accountService.GetInfoUser(token);

                foreach (var item in lessionRequest.ListClassId)
                {
                    var lession = new Lession
                    {
                        Name = lessionRequest.Title,
                        Description = lessionRequest.Description,
                        Status = lessionRequest.Status,
                        TitleId = lessionRequest.TitleId,
                        ClassId = item
                    };
                    await _lessionRepository.Add(lession);

                    _context.SaveChanges();

                    var fileInfo = await _documentService.PostFileAsync(fileData, fileType, lessionRequest.type);

                    _context.DocumentLessions.Add(new DocumentLession
                    {
                        DocumentId = fileInfo.Id,
                        LessionId = lession.Id
                    });

                    foreach (int i in lessionRequest.ListDocumentId)
                    {
                        _context.DocumentLessions.Add(new DocumentLession
                        {
                            DocumentId = i,
                            LessionId = lession.Id
                        });
                    }

                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LessionResponseDto> GetByTitleAndClass(int titleId, string classId)
        {
            return await _lessionRepository.GetByTitleAndClass(titleId, classId);
        }
    }
}
