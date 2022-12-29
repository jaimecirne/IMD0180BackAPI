using AutoMapper;
using IMD0180BackAPI.DTO;
using IMD0180BackAPI.Model;

namespace IMD0180BackAPI.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, CreateUserDTO>();
            CreateMap<User, ShowUserDTO>();
            CreateMap<User, UpdateUserDTO>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}
