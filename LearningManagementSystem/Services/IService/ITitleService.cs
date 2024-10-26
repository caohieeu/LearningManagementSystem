using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services.IService
{
    public interface ITitleService
    {
        Task<IEnumerable<Title>> GetAll();
        Task<IEnumerable<Title>> GetBySubject(string id);
        Task<bool> InsertTitle(TitleRequestDto title);
    }
}
