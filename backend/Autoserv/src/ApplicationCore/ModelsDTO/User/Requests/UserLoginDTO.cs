using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.ModelsDTO.User.Requests
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
