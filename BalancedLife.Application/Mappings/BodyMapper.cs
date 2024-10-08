using AutoMapper;
using BalancedLife.Application.DTOs.Body;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class BodyMapper : Profile {
        public BodyMapper() {
            CreateMap<Body, BodyDTO>().ReverseMap();
        }
    }
}
