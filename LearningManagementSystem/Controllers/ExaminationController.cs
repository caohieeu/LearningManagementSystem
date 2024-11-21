using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.AspNetCore.Http;
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
        [HttpPost]
        public async Task<IActionResult> AddAxam(ExaminationRequestDto exam)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.AddExam(exam)
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _examinationService.DeleteExam(id)
            });
        }
    }
}
