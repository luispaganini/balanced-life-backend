namespace BalancedLife.Domain.Entities {
    public class MealInfo : Meal{
        public double Carbohydrates { get; set; }
        public double Calories { get; set; }
        public double Colesterol { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Others { get; set; }
        public double TotalCalories { get; set; }
    }
}
