using AutoMapper;
using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Mappings {
    public class SnackMapper : Profile {
        public SnackMapper() {
            CreateMap<Meal, MealDTO>()
                .ForMember(dest => dest.TypeSnack, opt => opt.MapFrom(src => src.IdTypeSnackNavigation))
                .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation ?? ""))
                .ForMember(dest => dest.Snacks, opt => opt.MapFrom(src => src.Snacks));

            CreateMap<MealInfo, MealInfoDTO>()
                .ForMember(dest => dest.Carbohydrates, opt => opt.MapFrom(src => src.Carbohydrates))
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Calories))
                .ForMember(dest => dest.Colesterol, opt => opt.MapFrom(src => src.Colesterol))
                .ForMember(dest => dest.Fat, opt => opt.MapFrom(src => src.Fat))
                .ForMember(dest => dest.Protein, opt => opt.MapFrom(src => src.Protein))
                .ForMember(dest => dest.Others, opt => opt.MapFrom(src => src.Others))
                .ForMember(dest => dest.TypeSnack, opt => opt.MapFrom(src => src.IdTypeSnackNavigation))
                .ForMember(dest => dest.TotalCalories, opt => opt.MapFrom(src => src.TotalCalories));
            CreateMap<Snack, SnackDTO>()
                .ForMember(dest => dest.TypeSnack, opt => opt.MapFrom(src => src.IdTypeSnackNavigation))
                .ForMember(dest => dest.Food, opt => opt.MapFrom(src => src.IdFoodNavigation))
                .ForMember(dest => dest.UnitMeasurement, opt => opt.MapFrom(src => src.IdUnitMeasurementNavigation));
            CreateMap<Food, FoodDTO>()
                .ForMember(dest => dest.FoodNutritionInfo, opt => opt.MapFrom(src => src.FoodNutritionInfos))
                .ForMember(dest => dest.IdReferenceTable, opt => opt.MapFrom(src => src.ReferenceTable.Id))
                .ForMember(dest => dest.ReferenceTable, opt => opt.MapFrom(src => src.ReferenceTable.ReferenceTable1));

            CreateMap<FoodDTO, Food>()
                .ForMember(dest => dest.FoodNutritionInfos, opt => opt.MapFrom(src => src.FoodNutritionInfo));

            CreateMap<FoodNutritionInfo, FoodNutritionInfoDTO>()
                .ForMember(dest => dest.UnitMeasurement, opt => opt.MapFrom(src => src.IdUnitMeasurementNavigation))
                .ForMember(dest => dest.NutritionalComposition, opt => opt.MapFrom(src => src.IdNutritionalCompositionNavigation))
                .ReverseMap()
                .ForMember(dest => dest.IdUnitMeasurement, opt => opt.MapFrom(src => src.UnitMeasurement != null ? src.UnitMeasurement.Id : 0))
                .ForMember(dest => dest.IdUnitMeasurementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IdNutritionalComposition, opt => opt.MapFrom(src => src.NutritionalComposition != null ? src.NutritionalComposition.Id : 0))
                .ForMember(dest => dest.IdNutritionalCompositionNavigation, opt => opt.Ignore());


            CreateMap<UnitMeasurement, UnitMeasurementDTO>().ReverseMap();
            CreateMap<NutritionalComposition, NutritionalCompositionDTO>().ReverseMap();

            CreateMap<SnacksByDay, SnacksByDayDTO>()
                .ForMember(dest => dest.NameUser, opt => opt.MapFrom(src => src.NameUser))
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
                .ForMember(dest => dest.TotalCalories, opt => opt.MapFrom(src => src.TotalCalories))
                .ForMember(dest => dest.IdMeal, opt => opt.MapFrom(src => src.IdMeal))
                .ForMember(dest => dest.StatusSnack, opt => opt.MapFrom(src => src.StatusSnack));

            CreateMap<ReferenceTable, ReferenceTableDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReferenceTable, opt => opt.MapFrom(src => src.ReferenceTable1))
                .ForMember(dest => dest.Foods, opt => opt.MapFrom(src => src.Foods));   

            CreateMap<SnackFullDTO, Snack>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdFood, opt => opt.MapFrom(src => src.IdFood))
                .ForMember(dest => dest.IdTypeSnack, opt => opt.MapFrom(src => src.IdTypeSnack))
                .ForMember(dest => dest.Appointment, opt => opt.MapFrom(src => src.Appointment))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.IdMeal, opt => opt.MapFrom(src => src.IdMeal))
                .ForMember(src => src.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(src => src.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IdUnitMeasurement, opt => opt.MapFrom(src => src.IdUnitMeasurement));

            CreateMap<MealStatusDTO, MealStatus>()
                .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
