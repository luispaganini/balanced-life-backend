using AutoMapper;
using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class TypeSnackMapper : Profile {
        public TypeSnackMapper() {
            CreateMap<TypeSnack, TypeSnackDTO>().ReverseMap();
        }
    }
}
