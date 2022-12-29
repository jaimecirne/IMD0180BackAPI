using System.ComponentModel.DataAnnotations;

namespace IMD0180BackAPI.DTO
{
    public class CreateUserDTO
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
