namespace BalancedLife.Application.DTOs.Snack {
    public class FoodSearchDTO {
        public int QuantityPages { get; set; }
        public IEnumerable<FoodDTO> Foods { get; set; }
    }
}
