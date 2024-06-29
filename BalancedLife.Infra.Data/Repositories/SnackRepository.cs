using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class SnackRepository : ISnackRepository {
        private readonly ApplicationDbContext _context;

        public SnackRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Task<object> Add(object snack) {
            throw new NotImplementedException();
        }

        public Task<object> GetSnackById(int id) {
            throw new NotImplementedException();
        }

        public async Task<SnacksByDay> GetSnacksByDate(DateTime date, int userId) {
            var snacks = await _context.Snacks
                .Where(s => s.Appointment.Value.Date == date.Date && s.IdUser == userId)
                .Include(s => s.IdFoodNavigation)
                    .ThenInclude(fn => fn.FoodNutritionInfos)
                        .ThenInclude(fni => fni.IdUnitMeasurementNavigation)
                .Include(s => s.IdTypeSnackNavigation)
                .ToListAsync();

            var carbohydrates = 0.0;
            var calories = 0.0;
            var colesterol = 0.0;
            var protein = 0.0;
            var others = 0.0;
            var totalCalories = 0.0;

            foreach ( var snack in snacks ) {
                foreach ( var nutritionInfo in snack.IdFoodNavigation.FoodNutritionInfos ) {
                    switch ( nutritionInfo.IdNutritionalCompositionNavigation.Item ) {
                        case "Carboidrato total":
                            carbohydrates += nutritionInfo.Quantity ?? 0;
                            break;
                        case "Energia":
                            calories += nutritionInfo.Quantity ?? 0;
                            break;
                        case "Colesterol":
                            colesterol += nutritionInfo.Quantity ?? 0;
                            break;
                        case "Proteína":
                            protein += nutritionInfo.Quantity ?? 0;
                            break;
                        default:
                            others += nutritionInfo.Quantity ?? 0;
                            break;
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
                Snacks = snacks.Select(s => new Snacks {
                    TypeSnacks = s.IdTypeSnackNavigation,
                    Id = s.Id,
                    Title = s.IdTypeSnackNavigation.Name,
                    TotalCalories = s.IdFoodNavigation.FoodNutritionInfos
                        .Where(fni => fni.IdNutritionalCompositionNavigation.Item == "Energia")
                        .Sum(fni => fni.Quantity ?? 0)
                }).ToList()
            };

            return snacksByDay;
        }

        public Task<object> Update(object snack) {
            throw new NotImplementedException();
        }
    }
}
