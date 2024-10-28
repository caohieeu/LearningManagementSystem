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
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Data");
            }
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
            if (title == null)
            {
                return BadRequest("Invalid Data");
            }
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _titleService.InsertTitle(title)
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTitle([FromBody] TitleRequestDto title, int id)
        {
            if(title == null)
            {
                return BadRequest("Invalid Data");
            }

            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _titleService.UpdateTitle(title, id)
            });
        }
        [HttpDelete("")]
        public async Task<IActionResult> DeleteTitle([FromRoute] int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _titleService.RemoveTitle(id)
            });
        }
    }
}
