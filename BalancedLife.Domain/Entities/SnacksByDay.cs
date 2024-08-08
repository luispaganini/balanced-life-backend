using BalancedLife.Domain.Enums;

namespace BalancedLife.Domain.Entities {
    public class SnacksByDay {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string NameUser { get; set; }
        public double Carbohydrates { get; set; }
        public double Calories { get; set; }
        public double Colesterol { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Others { get; set; }
        public double TotalCalories { get; set; }
        public List<Snacks> Snacks { get; set; }
    }

    public class Snacks
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public TypeSnack TypeSnacks { get; set; }
        public double TotalCalories { get; set; }
        public long IdMeal { get; set; }
        public StatusMeal StatusSnack { get; set; }
    }
}
