using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
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
    }
}
