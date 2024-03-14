using AutoMapper;
using BalancedLife.Application.DTOs;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class UserMapper : Profile {
        public UserMapper() {
            CreateMap<UserInfo, UserInfoDTO>().ReverseMap();
            CreateMap<UserDTO, UserInfo>().ReverseMap();
        }
    }
}
