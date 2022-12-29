using System;

namespace IMD0180BackAPI.DTO
{
    public class ShowUserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public DateTime Lastlogin { get; set; }
    }
}
