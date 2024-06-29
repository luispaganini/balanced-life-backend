namespace BalancedLife.Application.DTOs.Snack {
    public class SnacksByDayDTO {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Carbohydrates { get; set; }
        public double Calories { get; set; }
        public double Colesterol { get; set; }
        public double Protein { get; set; }
        public double Others { get; set; }
        public double TotalCalories { get; set; }
        public List<SnackListDTO> Snacks { get; set; }
    }

    public class SnackListDTO {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
