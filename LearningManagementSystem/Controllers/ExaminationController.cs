using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        public ExaminationController(IExaminationService examinationService)
        {
            _examinationService = examinationService;
        }
        [HttpGet]
        [Authorize(Policy = "ViewExaminationPermission")]
        public async Task<IActionResult> GetExam([FromQuery] PaginationParams pagination, 
            string? DepartmentId, string? SubjectId, string? ExamName)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.GetExamination(pagination, DepartmentId,
                SubjectId, ExamName)
            });
        }
        [HttpGet("{ExamId}")]
        public async Task<IActionResult> GetExamDetail(int ExamId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.GetDetailExamination(ExamId)
            });
        }
        [HttpPost]
        [Authorize(Policy = "AddExaminationPermission")]
        public async Task<IActionResult> AddAxam(ExaminationRequestDto exam)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.AddExam(exam)
            });
        }

        [Authorize(Roles = Roles.LeaderShip)]
        [HttpPut("Approve/{ExamId}")]
        public async Task<IActionResult> ApproveExamination(int ExamId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.ApproveExamination(ExamId)
            });
        }

        [Authorize(Roles = Roles.LeaderShip)]
        [HttpPut("CancelApprove/{ExamId}")]
        public async Task<IActionResult> CancelApproveExamination(int ExamId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.CancelApproveExamination(ExamId)
            });
        }
        [HttpPost("CreateFromQuestionBank")]
        public async Task<IActionResult> CreateExamFromQuestionBank(ExQuestionBankRequestDto exam)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.AddFromQuestionBank(exam)
            });
        }
        [HttpDelete("{ExamId}")]
        [Authorize(Policy = "DeleteExaminationPermission")]
        public async Task<IActionResult> DeleteExam(int ExamId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.DeleteExam(ExamId)
            });
        }
        [HttpGet("GetQuestion/{ExamId}")]
        public async Task<IActionResult> GetQuestionExamination(int ExamId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.GetInformationExamination(ExamId)
            });
        }
        [HttpGet("GetQuestionsByFilter")]
        public async Task<IActionResult> GetQuestionsByFilter([FromQuery] FilterQExaminationDto filter, 
            [FromQuery] PaginationParams paginationParams)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.GetQuestionByFilter(filter, paginationParams)
            });
        }
        [HttpPost("AddQuestionBank")]
        public async Task<IActionResult> AddQuestionBank(QuestionBankRequestDto questionAxamRequestDto)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.AddQuestionBank(questionAxamRequestDto)
            });
        }
        [HttpPut("UpdateQuestionBank/{QuestionId}")]
        public async Task<IActionResult> UpdateQuestionBank(QuestionBankRequestDto questionAxamRequestDto, int QuestionId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.UpdateQuestionBank(questionAxamRequestDto, QuestionId)
            });
        }
        [HttpDelete("DeleteQuestionBank")]
        public async Task<IActionResult> DeleteQuestionBank(int QuestionId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.DeleteQuestionBank(QuestionId)
            });
        }
    }
}
