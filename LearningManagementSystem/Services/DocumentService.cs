using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserContext _userContext;
        public DocumentService(
            IDocumentRepository documentRepository,
            LMSContext context,
            IMapper mapper,
            IAccountService accountService,
            IUserContext userContext)
        {
            _documentRepository = documentRepository;
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
            _userContext = userContext;
        }
        public async Task<Document> PostFileAsync(IFormFile fileData, FileType fileType, 
            string type)
        {
            try
            {
                var currentUser = await _userContext.GetCurrentUserInfo();

                var document = new Document()
                {
                    FileName = fileData.FileName,
                    FileType = fileType,
                    Size = fileData.Length,
                    LastUpdate = DateTime.Now,
                    Type = type,
                    Approver = "",
                    SentDate = DateTime.Now,
                    Status = "chờ phê duyệt",    
                    IsAprroved = false,
                    CreatedBy = currentUser.FullName,
                    EditBy = currentUser.FullName,
                };

                using (var stream = new MemoryStream())
                {
                    await fileData.CopyToAsync(stream);
                    document.FileData = stream.ToArray();
                }

                var result = _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                return document;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostMultiFileAsync(List<FileUploadDto> fileData)
        {
            try
            {
                foreach (FileUploadDto file in fileData)
                {
                    var fileDetails = new Document()
                    {
                        FileName = file.FileDetails.FileName,
                        FileType = file.FileType,
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

                    var result = _context.Documents.Add(fileDetails);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DownloadFileById(int Id)
        {
            try
            {
                var file = _context.Documents.Where(x => x.Id == Id).FirstOrDefaultAsync();

                var content = new System.IO.MemoryStream(file.Result.FileData);
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "FileDownloaded",
                   file.Result.FileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                await CopyStream(content, path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
        public async Task<IEnumerable<Document>> GetAllDocument()
        {
            return await _documentRepository.GetAll();
        }

        public async Task<Document> UpdateName(int id, string name)
        {
            try
            {
                var document = await _documentRepository.GetById(id);

                if (document == null)
                {
                    throw new NotFoundException("Không tìm thấy tài nguyên");
                }

                document.FileName = name;
                document.LastUpdate = DateTime.Now;
                document.EditBy = await _userContext.GetFullName();

                await _documentRepository.Update(document);

                return document;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoveDocument(int id)
        {
            try
            {
                var document = await _documentRepository.GetById(id);

                if(document == null)
                {
                    throw new NotFoundException("Không tìm thấy tài nguyên");
                }

                await _documentRepository.Remove(document);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ApproveDocument(int id)
        {
            try
            {
                var document = await _documentRepository.GetById(id);

                if (document == null)
                {
                    throw new NotFoundException("Không tìm thấy tài nguyên");
                }

                document.IsAprroved = true;
                document.Approver = await _userContext.GetFullName();
                document.LastUpdate = DateTime.Now;
                document.EditBy = await _userContext.GetFullName();
                document.Status = "Đã phê duyệt";

                await _documentRepository.Update(document);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Document>> GetDocumentBySubject(string subjectId)
        {
            return await _documentRepository.GetDocumentBySubject(subjectId);
        }

        public async Task<bool> InsertNewResource(DocumentRequestDto document)
        {
            try
            {
                int documentInsert;
                if (document.FileUpload != null)
                {
                    var docInfo = await PostFileAsync(document.FileUpload.FileDetails, document.FileUpload.FileType, "tai-nguyen");
                    documentInsert = docInfo.Id;
                }
                else
                {
                    documentInsert = document.DocumentId;
                }
                _context.DocumentLessions.Add(new DocumentLession
                {
                    DocumentId = documentInsert,
                    LessionId = document.LessionId,
                });

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Document>> FindDocumentByName(string documentName)
        {
            return await _documentRepository.FindDocumentByName(documentName);
        }

        public async Task<bool> CancelApproveDocument(int documentId)
        {
            try
            {
                var document = await _documentRepository.GetById(documentId);

                if (document == null)
                {
                    throw new NotFoundException("Không tìm thấy tài nguyên");
                }

                document.IsAprroved = true;
                document.Approver = await _userContext.GetFullName();
                document.LastUpdate = DateTime.Now;
                document.EditBy = await _userContext.GetFullName();
                document.Status = "Đã hủy";

                await _documentRepository.Update(document);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Document>> GetResoucesByTitleAndClass(int titleId, string classId)
        {
            return await _documentRepository.GetResoucesByTitleAndClass(titleId, classId);
        }
    }
}
