using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly ITitleService _titleService;
        public TitleController(ITitleService titleService)
        {
            _titleService = titleService;
        }
        [HttpGet("GetBySubject/{id}")]
        public async Task<IActionResult> GetTitleBySubject([FromRoute] string id)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _titleService.GetBySubject(id)
            });
        }
        [HttpPost("")]
        public async Task<IActionResult> AddTitle([FromBody] TitleRequestDto title)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _titleService.InsertTitle(title)
            });
        }
    }
}
