using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Services.IService
{
    public interface ILessionService
    {
        Task<bool> InsertLession(LessionRequestDto lessionRequest,
            IFormFile fileData, FileType fileType);
        Task<LessionResponseDto> GetByTitleAndClass(int titleId, string classId);
    }
}
