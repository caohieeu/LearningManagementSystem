using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "ViewSubjectPermission")]
        [HttpGet]
        public async Task<ResponseEntity> GetAll()
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.GetAllSubject()
            };
        }
        [Authorize(Policy = "ViewSubjectPermission")]
        [HttpGet("GetDetail")]
        public async Task<ResponseEntity> GetSubjectDetail(string subjectId, string classId)
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.GetSubjectDetail(subjectId, classId)
            };
        }
        [Authorize(Policy = "ViewSubjectPermission")]
        [HttpGet("GetSubjectsByUser")]
        public async Task<ResponseEntity> GetSubjectsByUser()
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.GetSubjectsByUser()
            };
        }
        [Authorize(Roles = Utils.Roles.LeaderShip + ", " + Utils.Roles.Teacher)]
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] SubjectRequestDto subject)
        {
            if(await _subjectService.AddSubject(subject))
            {
                return Ok(new ResponseEntity
                {
                    code = ErrorCode.NoError.GetErrorInfo().code,
                    message = ErrorCode.NoError.GetErrorInfo().message,
                });
            }
            return BadRequest(new ResponseEntity
            {
                code = ErrorCode.Error.GetErrorInfo().code,
                message = ErrorCode.Error.GetErrorInfo().message,
            });
        }
        [Authorize(Policy = "EditSubjectPermission")]
        [HttpPut("{subjectId}")]
        public async Task<ResponseEntity> UpdateSubject([FromBody] SubjectRequestDto subject, string subjectId)
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.UpdateSubject(subject, subjectId)
            };
        }
        [Authorize(Roles = Utils.Roles.LeaderShip + ", " + Utils.Roles.Teacher)]
        [HttpPost("AssignSubject")]
        public async Task<ResponseEntity> AssignSubject([FromBody] AssignSubjectDto subject)
        {
            return new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _subjectService.AssignSubjectToStudent(subject)
            };
        }
    }
}
