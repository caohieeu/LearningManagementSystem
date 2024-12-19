using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using Microsoft.Data.SqlClient.DataClassification;

namespace LearningManagementSystem.Services.IService
{
    public interface ITitleService
    {
        Task<IEnumerable<Title>> GetAll();
        Task<IEnumerable<TitleResponseDto>> GetBySubject(string subjectId);
        Task<bool> InsertTitle(TitleRequestDto title);
        Task<bool> UpdateTitle(TitleRequestDto title, int id);
        Task<bool> RemoveTitle(int id);
    }
}
