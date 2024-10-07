using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Enums;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Domain.Utils;
using BalancedLife.Infra.Data.Context;
using BalancedLife.Infra.Data.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BalancedLife.Infra.Data.Repositories {
    public class SnackRepository : ISnackRepository {
        private readonly ApplicationDbContext _context;

        public SnackRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Meal> AddMeal(Meal meal) {
            await EntityHelper.LoadNavigationPropertyAsync(meal, s => s.IdTypeSnackNavigation, meal.IdTypeSnack, _context.TypeSnacks);

            var result = await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Snack> AddSnack(Snack snack) {
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdFoodNavigation, snack.IdFood, _context.Foods);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdUnitMeasurementNavigation, snack.IdUnitMeasurement, _context.UnitMeasurements);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdMealNavigation, snack.IdMeal, _context.Meals);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdTypeSnackNavigation, snack.IdTypeSnack, _context.TypeSnacks);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.CreatedByNavigation, snack.CreatedBy, _context.UserInfos);

            var result = await _context.Snacks.AddAsync(snack);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteSnack(long id) {
            var snack = await _context.Snacks.FindAsync(id);
            _context.Snacks.Remove(snack);

            await _context.SaveChangesAsync();
        }

        public async Task<MealInfo> GetMealById(int idMeal, int idTypeSnack, int idUser) {
            var carbohydrates = 0.0;
            var calories = 0.0;
            var colesterol = 0.0;
            var protein = 0.0;
            var others = 0.0;
            var totalCalories = 0.0;
            var fat = 0.0;

            var meal = await _context.Meals
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdFoodNavigation)
                        .ThenInclude(fn => fn.FoodNutritionInfos)
                            .ThenInclude(fni => fni.IdUnitMeasurementNavigation)
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdFoodNavigation)
                        .ThenInclude(fn => fn.FoodNutritionInfos)
                            .ThenInclude(fni => fni.IdNutritionalCompositionNavigation)
                .Include(m => m.Snacks)
                    .ThenInclude(s => s.IdTypeSnackNavigation)
                .FirstOrDefaultAsync(m => m.Id == idMeal);

            if ( meal == null ) {
                TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                DateTime brasiliaDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, brasiliaTimeZone);

                meal = new MealInfo {
                    Appointment = brasiliaDateTime,
                    IdUser = idUser,
                    IdTypeSnack = idTypeSnack,
                    Observation = "",
                    Status = (int?) StatusMeal.NotAwnsered,
                    Snacks = new List<Snack>()
                };

                meal = await AddMeal(meal);
            } else {
                foreach ( var snack in meal.Snacks.Where(s => s.IdTypeSnack == idTypeSnack) ) {
                    foreach ( var nutritionInfo in snack.IdFoodNavigation.FoodNutritionInfos ) {
                        var valuePer100g = nutritionInfo.Quantity ?? 0;
                        var adjustedValue = valuePer100g * SnackUtils.ConvertToGrams((double) snack.Quantity, snack.IdUnitMeasurementNavigation.Name) / 100;

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
                            case "Lipídios":
                                fat += adjustedValue;
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
            var mealInfo = new MealInfo {
                Id = meal.Id,
                Appointment = meal.Appointment,
                IdUser = meal.IdUser,
                IdTypeSnack = meal.IdTypeSnack,
                Observation = meal.Observation,
                Status = meal.Status,
                Snacks = meal.Snacks,
                Carbohydrates = Math.Round(carbohydrates, 2),
                Calories = Math.Round(calories, 2),
                Colesterol = Math.Round(colesterol, 2),
                Protein = Math.Round(protein, 2),
                Others = Math.Round(others, 2),
                Fat = Math.Round(fat, 2),
                TotalCalories = Math.Round(totalCalories, 2)
            };

            await EntityHelper.LoadNavigationPropertyAsync(mealInfo, s => s.IdTypeSnackNavigation, mealInfo.IdTypeSnack, _context.TypeSnacks);


            return mealInfo;
        }

        public async Task<SnacksByDay> GetSnacksByDate(DateTime date, int userId) {
            var carbohydrates = 0.0;
            var calories = 0.0;
            var colesterol = 0.0;
            var fat = 0.0;
            var protein = 0.0;
            var others = 0.0;
            var totalCalories = 0.0;
            var allTypeSnacks = await _context.TypeSnacks.ToListAsync();

            var userMeals = await _context.Meals
                .Where(m => m.IdUser == userId && m.Appointment.Date == date.Date)
                .ToListAsync();

            var mealIds = userMeals.Select(m => m.Id).ToList();

            var userSnacks = await _context.Snacks
                .Where(s => mealIds.Contains((long) s.IdMeal))
                .Include(s => s.IdFoodNavigation)
                    .ThenInclude(fn => fn.FoodNutritionInfos)
                        .ThenInclude(fni => fni.IdUnitMeasurementNavigation)
                .Include(s => s.IdFoodNavigation)
                    .ThenInclude(fn => fn.FoodNutritionInfos)
                        .ThenInclude(fni => fni.IdNutritionalCompositionNavigation)
                .ToListAsync();

            if ( userSnacks.Any() ) {
                foreach ( var snack in userSnacks ) {
                    foreach ( var nutritionInfo in snack.IdFoodNavigation.FoodNutritionInfos ) {
                        var valuePer100g = nutritionInfo.Quantity ?? 0;
                        var adjustedValue = 
                            valuePer100g * SnackUtils.ConvertToGrams((double) snack.Quantity, snack.IdUnitMeasurementNavigation.Name) / 100;

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
                            case "Lipídios":
                                fat += adjustedValue;
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

            var snacks = new List<Snacks>();

            foreach ( var ts in allTypeSnacks ) {
                var userSnacksOfType = userSnacks.Where(us => us.IdTypeSnack == ts.Id);

                var totalSnackCalories = 0.0;

                foreach ( var userSnack in userSnacksOfType ) {
                    var snackCalories = userSnack?.IdFoodNavigation.FoodNutritionInfos
                        .Where(fni => fni.IdNutritionalCompositionNavigation.Item == "Energia")
                        .Sum(fni => (fni.Quantity ?? 0) *
                            (SnackUtils.ConvertToGrams((double) userSnack.Quantity, userSnack.IdUnitMeasurementNavigation.Name) / 100)) ?? 0;

                    totalSnackCalories += snackCalories;
                }

                var meal = userMeals
                    .Where(i => i.Appointment.Date.Date == date.Date.Date)
                    .FirstOrDefault(m => m.IdTypeSnack == ts.Id);

                if ( meal == null ) {
                    DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(date);

                    meal = new MealInfo {
                        Appointment = utcDateTime,
                        IdUser = userId,
                        IdTypeSnack = ts.Id,
                        Observation = "",
                        Status = (int?) StatusMeal.NotAwnsered,
                        Snacks = new List<Snack>()
                    };

                    meal = await AddMeal(meal);
                }

                snacks.Add(new Snacks {
                    TypeSnacks = ts,
                    Id = ts.Id,
                    Title = ts.Name,
                    TotalCalories = Math.Round((double) totalSnackCalories, 2),
                    IdMeal = meal?.Id ?? 0,
                    StatusSnack = (StatusMeal) meal.Status

                });
            }

            var user = await _context.UserInfos.FindAsync((long)userId);

            var snacksByDay = new SnacksByDay {
                Date = date,
                UserId = userId,
                NameUser = user != null ? user.Name : "",
                Fat = Math.Round(fat, 2),
                Carbohydrates = Math.Round(carbohydrates, 2),
                Calories = Math.Round(calories, 2),
                Colesterol = Math.Round(colesterol, 2),
                Protein = Math.Round(protein, 2),
                Others = Math.Round(others, 2),
                TotalCalories = Math.Round(totalCalories, 2),
                Snacks = snacks
            };

            return snacksByDay;
        }

        public async Task<Meal> UpdateMeal(Meal meal) {
            var result = _context.Meals.Update(meal);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task UpdateMealStatus(long idMeal, MealStatus mealStatus) {
            var meal = _context.Meals.Find(idMeal);
            meal.Status = (int?) mealStatus.Status;
            meal.Observation = mealStatus.Observation;

            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task<Snack> UpdateSnack(Snack snack) {
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdFoodNavigation, snack.IdFood, _context.Foods);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdUnitMeasurementNavigation, snack.IdUnitMeasurement, _context.UnitMeasurements);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdMealNavigation, snack.IdMeal, _context.Meals);
            await EntityHelper.LoadNavigationPropertyAsync(snack, s => s.IdTypeSnackNavigation, snack.IdTypeSnack, _context.TypeSnacks);

            var result = _context.Snacks.Update(snack);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
