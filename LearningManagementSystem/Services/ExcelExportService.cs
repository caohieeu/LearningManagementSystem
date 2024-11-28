using LearningManagementSystem.DAL;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace LearningManagementSystem.Services
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly LMSContext _context;
        public ExcelExportService(LMSContext context)
        {
            _context = context;
        }
        public async Task<byte[]> ExportQuestionsToExcel(List<QuestionExam> questions, string examName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Câu hỏi");

                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Nội dung câu hỏi";
                worksheet.Cells[1, 3].Value = "Độ khó";
                worksheet.Cells[1, 4].Value = "Đáp án";
                worksheet.Cells[1, 5].Value = "Đáp án đúng";

                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                for (int i = 0; i < questions.Count; i++)
                {
                    var question = questions[i];
                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = question.QuestionText;
                    worksheet.Cells[i + 2, 3].Value = ((int)question.Level) == 1 ? "Dễ" : ((int)question.Level) == 2 ? "Trung bình" : "Khó";

                    var answers = await _context.AnswerExams
                            .Where(q => q.QuestionExamId == question.Id)
                            .ToListAsync();
                    var answersText = answers.Select(a => a.AnswerText).ToList();
                    worksheet.Cells[i + 2, 4].Value = string.Join(", ", answersText);

                    var correctAnswer = answers
                        .Where(a => a.IsCorrect)
                        .Select(a => a.AnswerText)
                        .ToList();
                    worksheet.Cells[i + 2, 5].Value = string.Join(", ", correctAnswer);
                }

                worksheet.Cells.AutoFitColumns();
                return package.GetAsByteArray();
            }
        }
    }
}
