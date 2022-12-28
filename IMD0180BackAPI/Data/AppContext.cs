using IMD0180BackAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace IMD0180BackAPI.Data
{
    public class UserContex : DbContext
    {
        public UserContex(DbContextOptions<UserContex> opt) : base(opt)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
