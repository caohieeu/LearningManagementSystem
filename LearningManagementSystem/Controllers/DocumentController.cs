using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using System.Text.Json.Serialization;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadDto fileDetails, string type)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                {
                    token = token.Substring("Bearer ".Length).Trim();
                }
                return Ok(new ResponseEntity
                {
                    code = ErrorCode.NoError.GetErrorInfo().code,
                    message = ErrorCode.NoError.GetErrorInfo().message,
                    data = await _documentService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType, type)
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Multiple File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostMultipleFile")]
        public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadDto> fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _documentService.PostMultiFileAsync(fileDetails);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("DownloadFile")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await _documentService.DownloadFileById(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.GetAllDocument()
            });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> UpdateDocument([FromRoute] int id, string name)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.UpdateName(id, name)
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> DeleteDocument([FromRoute] int id)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.RemoveDocument(id)
            });
        }
        [Authorize(Roles = Utils.Roles.LeaderShip)]
        [HttpPut("Approve/{DocumentId}")]
        public async Task<IActionResult> ApproveDocument(int DocumentId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.ApproveDocument(DocumentId)
            });
        }
        [Authorize(Roles = Utils.Roles.LeaderShip)]
        [HttpPut("CancelApprove/{DocumentId}")]
        public async Task<IActionResult> CancelApproveDocument(int DocumentId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.CancelApproveDocument(DocumentId)
            });
        }
        [HttpGet("GetDocumentBySubject/{SubjectId}")]
        public async Task<IActionResult> GetDocumentBySubject(string SubjectId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.GetDocumentBySubject(SubjectId)
            });
        }
        [HttpPost("InsertNewResource")]
        public async Task<IActionResult> InsertNewResource([FromForm]DocumentRequestDto document)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.InsertNewResource(document)
            });
        }
        [HttpGet("FindByName/{DocumentName}")]
        public async Task<IActionResult> FindDocumentByName(string DocumentName)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _documentService.FindDocumentByName(DocumentName)
            });
        }
    }
}
