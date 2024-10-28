using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public DocumentService(
            IDocumentRepository documentRepository,
            LMSContext context,
            IMapper mapper)
        {
            _documentRepository = documentRepository;
            _context = context;
            _mapper = mapper;
        }
        public async Task PostFileAsync(IFormFile fileData, FileType fileType, string type)
        {
            try
            {
                var document = new Document()
                {
                    FileName = fileData.FileName,
                    FileType = fileType,
                    Size = fileData.Length,
                    LastUpdate = DateTime.Now,
                    Type = type,
                    Approver = "Cao Hiếu",
                    SentDate = DateTime.Now,
                    Status = "Đang chờ",
                    IsAprroved = false,
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    document.FileData = stream.ToArray();
                }

                var result = _context.Documents.Add(document);
                await _context.SaveChangesAsync();
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
                        Id = 0,
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
    }
}
