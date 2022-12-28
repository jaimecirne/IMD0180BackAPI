using System;

namespace IMD0180BackAPI.DTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
