using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos
{
    public class ConfirmPasswordDto
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "The password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
