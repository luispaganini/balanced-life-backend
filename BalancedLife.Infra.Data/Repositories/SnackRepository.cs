using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BalancedLife.Infra.Data.Repositories {
    public class SnackRepository : ISnackRepository {
        private readonly ApplicationDbContext _context;

        public SnackRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Task<object> Add(object snack) {
            throw new NotImplementedException();
        }

        public async Task<Meal> GetMealById(int id) {
            var meal = await _context.Meals
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdFoodNavigation)
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdTypeSnackNavigation)
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdUnitMeasurementNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if ( meal != null ) 
                meal.Snacks = meal.Snacks.Where(s => s.IdTypeSnack == meal.IdTypeSnack).ToList();

            return meal;
        }

        public async Task<SnacksByDay> GetSnacksByDate(DateTime date, int userId) {
            var carbohydrates = 0.0;
            var calories = 0.0;
            var colesterol = 0.0;
            var protein = 0.0;
            var others = 0.0;
            var totalCalories = 0.0;
            var allTypeSnacks = await _context.TypeSnacks.ToListAsync();

            var userMeals = await _context.Meals
                .Where(m => m.IdUser == userId && m.Appointment.Date == date.Date)
                .ToListAsync();

            var mealIds = userMeals.Select(m => (long?) m.Id);

            var userSnacks = await _context.Snacks
                .Where(s => mealIds.Contains(s.IdMeal))
                .Include(s => s.IdFoodNavigation)
                    .ThenInclude(fn => fn.FoodNutritionInfos)
                        .ThenInclude(fni => fni.IdUnitMeasurementNavigation)
                .Include(s => s.IdFoodNavigation)
                    .ThenInclude(fn => fn.FoodNutritionInfos)
                        .ThenInclude(fni => fni.IdNutritionalCompositionNavigation)
                .ToListAsync();

            if ( userSnacks.Any() ) {
                foreach ( var snack in userSnacks ) {
                    var quantity = snack.Quantity;
                    foreach ( var nutritionInfo in snack.IdFoodNavigation.FoodNutritionInfos ) {
                        var valuePer100g = nutritionInfo.Quantity ?? 0;
                        var adjustedValue = (valuePer100g * quantity) / 100 ?? 0;

                        switch ( nutritionInfo.IdNutritionalCompositionNavigation.Item ) {
                            case "Carboidrato total":
                                carbohydrates += adjustedValue;
                                break;
                            case "Energia":
                                calories += adjustedValue;
                                totalCalories += adjustedValue;
                                break;
                            case "Colesterol":
                                colesterol += adjustedValue;
                                break;
                            case "Proteína":
                                protein += adjustedValue;
                                break;
                            default:
                                others += adjustedValue;
                                break;
                        }
                    }
                }
            }

            var snacksByDay = new SnacksByDay {
                Date = date,
                UserId = userId,
                Carbohydrates = carbohydrates,
                Calories = calories,
                Colesterol = colesterol,
                Protein = protein,
                Others = others,
                TotalCalories = totalCalories,
                Snacks = allTypeSnacks.Select(ts => {
                    var userSnack = userSnacks.FirstOrDefault(us => us.IdTypeSnack == ts.Id);
                    var totalSnackCalories = userSnack?.IdFoodNavigation.FoodNutritionInfos
                        .Where(fni => fni.IdNutritionalCompositionNavigation.Item == "Energia")
                        .Sum(fni => (fni.Quantity ?? 0) * (userSnack.Quantity / 100)) ?? 0;

                    return new Snacks {
                        TypeSnacks = ts,
                        Id = ts.Id,
                        Title = ts.Name,
                        TotalCalories = totalSnackCalories,
                        IdMeal = userSnack?.IdMeal ?? 0,
                    };
                }).ToList()
            };
            return snacksByDay;
        }

        public Task<object> Update(object snack) {
            throw new NotImplementedException();
        }
    }
}
