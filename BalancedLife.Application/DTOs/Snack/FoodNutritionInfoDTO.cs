namespace BalancedLife.Application.DTOs.Snack {
    public class FoodNutritionInfoDTO {
        public long Id { get; set; }
        public UnitMeasurementDTO UnitMeasurement { get; set; }
        public double Quantity { get; set; }
        public NutritionalCompositionDTO NutritionalComposition { get; set; }
    }
}
