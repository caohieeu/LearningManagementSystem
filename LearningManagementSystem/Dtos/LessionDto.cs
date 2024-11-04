using LearningManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Dtos
{
    public class LessionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int TitleId { get; set; }
        public string ClassId { get; set; }
    }
}
