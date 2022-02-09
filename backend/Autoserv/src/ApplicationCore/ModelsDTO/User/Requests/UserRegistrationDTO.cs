using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.ModelsDTO.User.Requests
{
    public class UserRegistrationDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
