using System.ComponentModel.DataAnnotations;

namespace EfLearning.Api.Resources.Account
{
    public class LoginResource
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}
