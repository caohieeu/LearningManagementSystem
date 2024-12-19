using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos
{
    public class SignUpDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string DepartmentId { get; set; }
    }
}
