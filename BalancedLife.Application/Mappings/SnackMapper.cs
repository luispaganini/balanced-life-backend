using AutoMapper;
using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class SnackMapper : Profile{
        public SnackMapper() {
            CreateMap<SnackDTO, Snack>().ReverseMap();
            CreateMap<SnacksByDay, SnacksByDayDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Carbohydrates, opt => opt.MapFrom(src => src.Carbohydrates))
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Calories))
                .ForMember(dest => dest.Colesterol, opt => opt.MapFrom(src => src.Colesterol))
                .ForMember(dest => dest.Protein, opt => opt.MapFrom(src => src.Protein))
                .ForMember(dest => dest.Others, opt => opt.MapFrom(src => src.Others))
                .ForMember(dest => dest.TotalCalories, opt => opt.MapFrom(src => src.TotalCalories))
                .ForMember(dest => dest.Snacks, opt => opt.MapFrom(src => src.Snacks));

            CreateMap<Snacks, SnackListDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TotalCalories, opt => opt.MapFrom(src => src.TotalCalories));
        }
    }
}
