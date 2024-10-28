//using FileUpload.Services;
//using LearningManagementSystem.Dtos;
//using LearningManagementSystem.Utils;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FileUpload.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FilesController : ControllerBase
//    {
//        private readonly IFileService _uploadService;

//        public FilesController(IFileService uploadService)
//        {
//            _uploadService = uploadService;
//        }

//        /// <summary>
//        /// Single File Upload
//        /// </summary>
//        /// <param name="file"></param>
//        /// <returns></returns>
//        [HttpPost("PostSingleFile")]
//        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadDto fileDetails)
//        {
//            if (fileDetails == null)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);
//                return Ok();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Multiple File Upload
//        /// </summary>
//        /// <param name="file"></param>
//        /// <returns></returns>
//        [HttpPost("PostMultipleFile")]
//        public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadDto> fileDetails)
//        {
//            if (fileDetails == null)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                await _uploadService.PostMultiFileAsync(fileDetails);
//                return Ok();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Download File
//        /// </summary>
//        /// <param name="file"></param>
//        /// <returns></returns>
//        [HttpGet("DownloadFile")]
//        public async Task<ActionResult> DownloadFile(int id)
//        {
//            if (id < 1)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                await _uploadService.DownloadFileById(id);
//                return Ok();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//        [HttpGet("GetAll")]
//        public async Task<ActionResult> GetAllFile()
//        {
//            try
//            {
//                return Ok(new ResponseEntity
//                {
//                    code = ErrorCode.NoError.GetErrorInfo().code,
//                    message = ErrorCode.NoError.GetErrorInfo().message,
//                    data= await _uploadService.GetAllFile()
//                });
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//    }
//}