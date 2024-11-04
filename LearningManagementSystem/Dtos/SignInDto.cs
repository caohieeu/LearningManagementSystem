using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos
{
    public class SignInDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //public string Role { get; set; }
    }
}
