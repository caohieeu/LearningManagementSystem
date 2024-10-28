//using AutoMapper;
//using LearningManagementSystem.DAL;
//using LearningManagementSystem.Dtos;
//using LearningManagementSystem.Models;
//using LearningManagementSystem.Repositories.IRepository;
//using LearningManagementSystem.Utils;
//using Microsoft.EntityFrameworkCore;

//namespace FileUpload.Services
//{
//    public class FileService : IFileService
//    {
//        private readonly LMSContext _context;
//        private readonly IFileDetailsRepository _fileDetailsRepository;
//        private readonly IMapper _mapper;
//        public FileService(
//            LMSContext context,
//            IFileDetailsRepository fileDetailsRepository,
//            IMapper mapper)
//        {
//            _context = context;
//            _fileDetailsRepository = fileDetailsRepository;
//            _mapper = mapper; 
//        }

//        public async Task PostFileAsync(IFormFile fileData, FileType fileType)
//        {
//            try
//            {
//                var fileDetails = new FileDetails()
//                {
//                    Id = 0,
//                    FileName = fileData.FileName,
//                    FileType = fileType,
//                    Size = fileData.Length,
//                    LastUpdate = DateTime.Now,
//                };

//                using (var stream = new MemoryStream())
//                {
//                    fileData.CopyTo(stream);
//                    fileDetails.FileData = stream.ToArray();
//                }

//                var result = _context.FileDetails.Add(fileDetails);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task PostMultiFileAsync(List<FileUploadDto> fileData)
//        {
//            try
//            {
//                foreach (FileUploadDto file in fileData)
//                {
//                    var fileDetails = new FileDetails()
//                    {
//                        Id = 0,
//                        FileName = file.FileDetails.FileName,
//                        FileType = file.FileType,
//                    };

//                    using (var stream = new MemoryStream())
//                    {
//                        file.FileDetails.CopyTo(stream);
//                        fileDetails.FileData = stream.ToArray();
//                    }

//                    var result = _context.FileDetails.Add(fileDetails);
//                }
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task DownloadFileById(int Id)
//        {
//            try
//            {
//                var file = _context.FileDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();

//                var content = new System.IO.MemoryStream(file.Result.FileData);
//                var path = Path.Combine(
//                   Directory.GetCurrentDirectory(), "FileDownloaded",
//                   file.Result.FileName);

//                Directory.CreateDirectory(Path.GetDirectoryName(path));

//                await CopyStream(content, path);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task CopyStream(Stream stream, string downloadPath)
//        {
//            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
//            {
//                await stream.CopyToAsync(fileStream);
//            }
//        }

//        public async Task<IEnumerable<FileDetails>> GetAllFile()
//        {
//            return await _fileDetailsRepository.GetAll();
//        }
//    }
//}