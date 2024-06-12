using AutoMapper;
using BalancedLife.Application.DTOs;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class UserMapper : Profile {
        public UserMapper() {
            CreateMap<UserInfo, UserInfoDTO>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.IdUserRoleNavigation))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new LocationDTO {
                    City = new CityDTO {
                        Id = src.IdCityNavigation.Id,
                        Name = src.IdCityNavigation.Name
                    },
                    State = new StateDTO {
                        Id = src.IdCityNavigation.IdStateNavigation.Id,
                        Name = src.IdCityNavigation.IdStateNavigation.Name,
                        Uf = src.IdCityNavigation.IdStateNavigation.Uf
                    }
                }))
                .ReverseMap();
            CreateMap<UserDTO, UserInfo>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
        }
    }
}
