namespace BalancedLife.Application.DTOs.Snack {
    public class SnackDTO {
        public long Id { get; set; }
        public TypeSnackDTO TypeSnack { get; set; }
        public float Quantity { get; set; }
        public UnitMeasurementDTO UnitMeasurement { get; set; }
        public FoodDTO Food { get; set; }
    }
}
