using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.DTOs.Snack {
    public class FoodDTO {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? IdFoodGroup { get; set; }

        public string ReferenceTable { get; set; }

        public string Brand { get; set; }
        public List<FoodNutritionInfoDTO>? FoodNutritionInfo { get; set; }
    }
}
