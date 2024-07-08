using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Enums;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Domain.Utils;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BalancedLife.Infra.Data.Repositories {
    public class SnackRepository : ISnackRepository {
        private readonly ApplicationDbContext _context;

        public SnackRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Meal> AddMeal(Meal meal) {
            if (meal.IdTypeSnackNavigation == null)
                meal.IdTypeSnackNavigation = await _context.TypeSnacks.FindAsync(meal.IdTypeSnack);

            var result = await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Meal> GetMealById(int idMeal, int idTypeSnack, int idUser) {
            var meal = await _context.Meals
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdFoodNavigation)
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdTypeSnackNavigation)
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdUnitMeasurementNavigation)
                .FirstOrDefaultAsync(m => m.Id == idMeal);

            if ( meal == null ) {
                meal = new Meal {
                    Appointment = DateTime.Now,
                    IdUser = idUser,
                    IdTypeSnack = idTypeSnack,
                    Observation = "",
                    Status = (int?) StatusMeal.NotAwnsered,
                    Snacks = new List<Snack>()
                };

                meal = await AddMeal(meal);
            } else 
                meal.Snacks = meal.Snacks.Where(s => s.IdTypeSnack == idTypeSnack).ToList();
            

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

            var mealIds = userMeals.Select(m => m.Id);

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
                        //var adjustedValue = (valuePer100g * quantity) / 100 ?? 0;
                        var adjustedValue = SnackUtils.ConvertTo100g(
                            snack.IdUnitMeasurementNavigation.Name, 
                            (double) (quantity != null ? quantity : 0), 
                            valuePer100g
                         );

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
                        TotalCalories = (double) totalSnackCalories,
                        IdMeal = userSnack?.IdMeal ?? 0,
                    };
                }).ToList()
            };
            return snacksByDay;
        }

        public async Task<Meal> UpdateMeal(Meal meal) {
            var result = _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
