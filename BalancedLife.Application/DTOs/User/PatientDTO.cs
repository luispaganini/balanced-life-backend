using BalancedLife.Domain.Enums;

namespace BalancedLife.Application.DTOs.User {
    public class PatientDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrentNutritionist { get; set; }
        public StatusNutritionist LinkStatus { get; set; }
        public int Age { get; set; }
    }
}
