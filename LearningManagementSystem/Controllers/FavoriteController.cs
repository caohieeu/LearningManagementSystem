using LearningManagementSystem.Services;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> PostFavoriteAnswer(int id, string type)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _favoriteService.PostFavorite(id, type)
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFavoriteAnswer(int id, string type)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _favoriteService.RemoveFavorite(id, type)
            });
        }
    }
}
