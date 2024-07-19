namespace BalancedLife.Application.DTOs.Snack {
    public class SnackFullDTO {
        public long? Id { get; set; }
        public long IdFood { get; set; }
        public long IdTypeSnack { get; set; }
        public DateTime? Appointment { get; set; }
        public double? Quantity { get; set; }
        public long? IdMeal { get; set; }
        public long? IdUnitMeasurement { get; set; }
    }
}
