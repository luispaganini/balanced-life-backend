using AutoMapper;
using BalancedLife.Application.DTOs;
using BalancedLife.Application.DTOs.User;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings
{
    public class UserMapper : Profile {
        public UserMapper() {
            CreateMap<UserInfo, UserInfoDTO>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.IdUserRoleNavigation))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.IdCityNavigation != null ? new LocationDTO {
                    City = src.IdCityNavigation != null ? new CityDTO {
                        Id = src.IdCityNavigation.Id,
                        Name = src.IdCityNavigation.Name
                    } : null,
                    State = src.IdCityNavigation.IdStateNavigation != null ? new StateDTO {
                        Id = src.IdCityNavigation.IdStateNavigation.Id,
                        Name = src.IdCityNavigation.IdStateNavigation.Name,
                        Uf = src.IdCityNavigation.IdStateNavigation.Uf,
                        Country = src.IdCityNavigation.IdStateNavigation.Country
                    } : null
                } : null))
                .ReverseMap();

            CreateMap<UserDTO, UserInfo>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<Patient, PatientDTO>().ReverseMap();
        }
    }
}
