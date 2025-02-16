namespace BalancedLife.Application.DTOs.Diet {
    public class DietDTO {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long IdNutritionist { get; set; }

        public long IdPatient { get; set; }
        public List<DietDayDTO>? DietDays { get; set; }
    }
}
