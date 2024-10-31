using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessionController : ControllerBase
    {
        private readonly ILessionService _lessionService;
        public LessionController(ILessionService lessionService)
        {
            _lessionService = lessionService;
        }
        [HttpGet]
        [Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> GetAllLession()
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = null
            });
        }
        [HttpGet("GetByTitleAndClass")]
        public async Task<IActionResult> GetLession(int TitleId, string ClassId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _lessionService.GetByTitleAndClass(TitleId, ClassId)
            });
        }
        [HttpGet("GetByTitle/{TitleId}")]
        public async Task<IActionResult> GetLessionsByTitle(int TitleId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = null
            });
        }
        [HttpPost]
        [Authorize(Roles = Utils.Roles.Teacher)]
        public async Task<IActionResult> AddLession([FromForm] LessionRequestDto lession, [FromForm] FileUploadDto fileDetails)
        {
            //var token = HttpContext.Request.Headers["Authorization"].ToString();

            //if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            //{
            //    token = token.Substring("Bearer ".Length).Trim();
            //}

            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _lessionService.InsertLession(lession, 
                fileDetails.FileDetails, fileDetails.FileType)
            });
        }
    }
}
