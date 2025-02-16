using AutoMapper;
using BalancedLife.Application.DTOs.Diet;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class DietMapper : Profile {
        public DietMapper() {
            CreateMap<Diet, DietDTO>().ReverseMap();
            CreateMap<DietDay, DietDayDTO>().ReverseMap();
            CreateMap<DaysOfWeek, DaysOfWeekDTO>().ReverseMap();
        }
    }
}
