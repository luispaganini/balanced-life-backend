namespace BalancedLife.Domain.Utils {
    public static class SnackUtils {
        public static double ConvertToGrams(double value, string unit) {
            switch ( unit ) {
                case "g":
                    return value;
                case "mg":
                    return value * 0.001;
                case "kg":
                    return value * 1000;
                case "µg":
                case "mcg":
                    return value * 1e-6;
                case "ng":
                    return value * 1e-9;
                case "dg":
                    return value * 0.1;
                case "hg":
                    return value * 100;
                case "oz":
                    return value * 28.3495;
                default:
                    return 0;
            }
        }

        public static double CalculateCalories(string nutrientName, double valueInGrams) {
            const double caloriesPerGramCarbohydrate = 4.0;
            const double caloriesPerGramProtein = 4.0;
            const double caloriesPerGramFat = 9.0;

            double calories = 0;

            switch ( nutrientName.ToLower() ) {
                case "carboidrato total":
                //case "carboidrato disponível":
                    calories = valueInGrams * caloriesPerGramCarbohydrate;
                    break;
                case "proteína":
                    calories = valueInGrams * caloriesPerGramProtein;
                    break;
                case "lipídios":
                    calories = valueInGrams * caloriesPerGramFat;
                    break;
            }

            return calories;
        }

    }
}
