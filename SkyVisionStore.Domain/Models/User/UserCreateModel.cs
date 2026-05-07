using SkyVisionStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkyVisionStore.Domain.Models.User
{
    public class UserCreateModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;
    }
}