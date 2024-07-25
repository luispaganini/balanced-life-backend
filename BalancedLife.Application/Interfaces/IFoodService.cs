using BalancedLife.Application.DTOs.Snack;

namespace BalancedLife.Application.Interfaces {
    public interface IFoodService {
        Task<FoodDTO> Add(FoodDTO food);
        Task<IEnumerable<FoodDTO>> FindFoodBySearch(string food, int pageNumber);
        Task<FoodDTO> GetFoodById(int id);
        Task<IEnumerable<UnitMeasurementDTO>> GetUnitsMeasurement();
        Task<IEnumerable<NutritionalCompositionDTO>> GetNutritionalCompositions();
    }
}
