using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetNotificationByUser([FromQuery] PaginationParams pagination, [FromQuery] bool? isRead)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _notificationService.GetPagedNotificationAsync(pagination, isRead)
            });
        }
        [HttpGet("GetBySubject")]
        public async Task<IActionResult> GetNotificationBySubject([FromQuery] PaginationParams pagination, [FromQuery] string subjectId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _notificationService.GetPagedNotificationBySubjectAsync(pagination, subjectId)
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateNotificationToStudent(NotificationSubRequestDto notificationRequestDto)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _notificationService.CreateNotificationToStudent(notificationRequestDto)
            });
        }
        [HttpPut("ReadNotification/{id}")]
        public async Task<IActionResult> ReadNotification(int id)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _notificationService.ReadNotification(id)
            }); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _notificationService.DeleteNotification(id)
            });
        }
    }
}
