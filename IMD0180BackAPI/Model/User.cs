using System;
using System.ComponentModel.DataAnnotations;

namespace IMD0180BackAPI.Model
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password  é obrigatório")]
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime Lastlogin { get; set; }
    }
}
