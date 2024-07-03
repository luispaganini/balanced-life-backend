using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Enums;

namespace BalancedLife.Application.DTOs.Snack {
    public class MealDTO {
        public long Id { get; set; }

        public long IdUser { get; set; }

        public DateTime Appointment { get; set; }

        public string Observation { get; set; }

        public StatusMeal Status { get; set; }

        public TypeSnackDTO TypeSnack { get; set; }
        public List<SnackDTO> Snacks { get; set; }
    }
}
