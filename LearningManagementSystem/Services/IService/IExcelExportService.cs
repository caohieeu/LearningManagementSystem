using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services.IService
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportQuestionsToExcel(List<QuestionExam> questions, string examName);
    }
}
