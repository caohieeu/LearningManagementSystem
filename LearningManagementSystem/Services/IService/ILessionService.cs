using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services.IService
{
    public interface ILessionService
    {
        Task<bool> InsertLession(Lession lession);
    }
}
