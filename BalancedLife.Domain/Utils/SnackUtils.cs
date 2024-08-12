using BalancedLife.Domain.Entities;

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
                    return value;
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

        public static SnackFormatted GetNutritionValues(ICollection<Snack> snacksNotFormatted) {
            var values = new List<NutritionalValue>();
            var Snack = new SnackFormatted();

            var nutrientMapping = new Dictionary<string, Action<NutritionalValue>> {
                { "energia", value => Snack.Energy = value },
                { "umidade", value => Snack.Moisture = value },
                { "carboidrato total", value => Snack.TotalCarbohydrate = value },
                { "carboidrato disponível", value => Snack.AvailableCarbohydrate = value },
                { "proteína", value => Snack.Protein = value },
                { "lipídios", value => Snack.Lipids = value },
                { "fibra alimentar", value => Snack.DietaryFiber = value },
                { "álcool", value => Snack.Alcohol = value },
                { "cinzas", value => Snack.Ash = value },
                { "colesterol", value => Snack.Cholesterol = value },
                { "ácidos graxos saturados", value => Snack.SaturatedFattyAcids = value },
                { "ácidos graxos monoinsaturados", value => Snack.MonounsaturatedFattyAcids = value },
                { "ácidos graxos poliinsaturados", value => Snack.PolyunsaturatedFattyAcids = value },
                { "ácidos graxos trans", value => Snack.TransFattyAcids = value },
                { "cálcio", value => Snack.Calcium = value },
                { "ferro", value => Snack.Iron = value },
                { "sódio", value => Snack.Sodium = value },
                { "magnésio", value => Snack.Magnesium = value },
                { "fósforo", value => Snack.Phosphorus = value },
                { "potássio", value => Snack.Potassium = value },
                { "manganês", value => Snack.Manganese = value },
                { "zinco", value => Snack.Zinc = value },
                { "cobre", value => Snack.Copper = value },
                { "selênio", value => Snack.Selenium = value },
                { "vitamina a (re)", value => Snack.VitaminA_RE = value },
                { "vitamina a (rae)", value => Snack.VitaminA_RAE = value },
                { "vitamina d", value => Snack.VitaminD = value },
                { "alfa-tocoferol (vitamina e)", value => Snack.AlphaTocopherol = value },
                { "tiamina", value => Snack.Thiamine = value },
                { "riboflavina", value => Snack.Riboflavin = value },
                { "niacina", value => Snack.Niacin = value },
                { "vitamina b6", value => Snack.VitaminB6 = value },
                { "vitamina b12", value => Snack.VitaminB12 = value },
                { "vitamina c", value => Snack.VitaminC = value },
                { "equivalente de folato", value => Snack.FolateEquivalent = value },
            };

            foreach ( var snack in snacksNotFormatted ) {
                foreach ( var nutritionInfo in snack.IdFoodNavigation.FoodNutritionInfos ) {
                    var valuePer100g = nutritionInfo.Quantity ?? 0;
                    var adjustedValue = valuePer100g * SnackUtils.ConvertToGrams((double) snack.Quantity, snack.IdUnitMeasurementNavigation.Name) / 100;

                    // Converte o tipo de nutriente para minúsculas e tenta obter a ação correspondente
                    var nutrientType = nutritionInfo.IdNutritionalCompositionNavigation.Item.ToLower();
                    if ( nutrientMapping.TryGetValue(nutrientType, out var setNutritionalValue) ) {
                        setNutritionalValue(new NutritionalValue {
                            UnitMeasurement = nutritionInfo.IdUnitMeasurementNavigation.Name,
                            Total = SnackUtils.CalculateCalories(nutritionInfo.IdNutritionalCompositionNavigation.Item, adjustedValue)
                        });
                    }
                }
            }

            return Snack;
        }
    }
}
