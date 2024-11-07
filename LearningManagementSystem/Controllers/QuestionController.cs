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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;  
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet("QuestionByLession/{LessionId}")]
        public async Task<IActionResult> GetQuestionsBySubject(int LessionId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _questionService.GetQuestionsBySubject(LessionId)
            });
        }
        [HttpGet("FindByTitle/{title}")]
        public async Task<IActionResult> GetQuestionsBySubject(string title)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _questionService.GetQuestionsByTitleName(title)
            });
        }
        [HttpPost("PostQuestion")]
        [Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> PostQuestion([FromBody]QuestionRequestDto question)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _questionService.InsertQuestion(question)
            });
        }
    }
}
