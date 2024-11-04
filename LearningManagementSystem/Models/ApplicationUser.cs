using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string Address { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
