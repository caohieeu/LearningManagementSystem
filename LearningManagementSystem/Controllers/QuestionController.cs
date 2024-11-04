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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;  
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion(QuestionRequestDto question)
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
