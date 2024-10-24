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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet("")]
        public async Task<ResponseEntity> GetAll()
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.GetAllSubject()
            };
        }
        [HttpPost("")]
        public async Task<ResponseEntity> AddSubject([FromBody] SubjectRequestDto subject)
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.AddSubject(subject)
            };
        }
        [HttpPut("")]
        public async Task<ResponseEntity> UpdateSubject([FromBody] SubjectRequestDto subject)
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.AddSubject(subject)
            };
        }
    }
}
