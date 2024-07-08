namespace BalancedLife.Domain.Utils {
    public static class SnackUtils {
        public static double ConvertTo100g(string unit, double quantity, double valuePerUnit) {
            double valuePer100g = 0;

            switch ( unit.ToLower() ) {
                case "g":
                    valuePer100g = valuePerUnit * quantity / 100;
                    break;
                case "kj":
                    valuePer100g = valuePerUnit * quantity / 10;
                    break;
                case "kcal":
                    valuePer100g = valuePerUnit * quantity;
                    break;
                case "mg":
                    valuePer100g = valuePerUnit * quantity / 100000;
                    break;
                case "l":
                    valuePer100g = valuePerUnit * quantity * 10;
                    break;
                case "kg":
                    valuePer100g = valuePerUnit * quantity * 10;
                    break;
                case "µg":
                    valuePer100g = valuePerUnit * quantity / 100000000;
                    break;
                case "un":
                case "pot":
                case "pkg":
                case "can":
                case "btl":
                case "box":
                case "bar":
                case "toast":
                    valuePer100g = valuePerUnit;
                    break;
                case "mm":
                case "cm":
                case "m":
                case "km":
                case "in":
                case "ft":
                case "yd":
                case "mi":
                    valuePer100g = valuePerUnit * quantity / 100;
                    break;
                case "gl":
                    valuePer100g = valuePerUnit * quantity / 100;
                    break;
                case "ng":
                    valuePer100g = valuePerUnit * quantity / 1000000000;
                    break;
                case "dg":
                    valuePer100g = valuePerUnit * quantity / 10;
                    break;
                case "hg":
                    valuePer100g = valuePerUnit * quantity * 10;
                    break;
                case "oz":
                    valuePer100g = valuePerUnit * quantity * 28.3495f / 100;
                    break;
                case "cup":
                    valuePer100g = valuePerUnit * quantity * 236.588f / 100;
                    break;
                case "tbsp":
                    valuePer100g = valuePerUnit * quantity * 14.7868f / 100;
                    break;
                case "tsp":
                    valuePer100g = valuePerUnit * quantity * 4.92892f / 100;
                    break;
                case "mcg":
                    valuePer100g = valuePerUnit * quantity / 1000000;
                    break;
                default:
                    break;
            }

            return valuePer100g;
        }
    }
}
