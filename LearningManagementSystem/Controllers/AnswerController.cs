using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IFavoriteService _favoriteService;
        public AnswerController(IAnswerService answerService, IFavoriteService favoriteService)
        {
            _answerService = answerService;
            _favoriteService = favoriteService;
        }
        [HttpPost]
        //[Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> PostAnswer([FromBody] AnswerRequestDto answer)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _answerService.InsertAnswer(answer)
            });
        }
        [HttpPut("{Id}")]
        //[Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> UpdateAnswer([FromBody] AnswerRequestDto answer, int Id)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _answerService.UpdateAnswer(answer, Id)
            });
        }
        [HttpDelete("{Id}")]
        //[Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> DeleteAnswer(int Id)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _answerService.DeleteAnswer(Id)
            });
        }
        [HttpGet("GetByQuestions/{QuestionId}")]
        //[Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> GetAnswerByQuestion(int QuestionId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _answerService.GetAnswerByQuestion(QuestionId)
            });
        }
    }
}
