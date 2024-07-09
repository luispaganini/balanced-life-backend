using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface ISnackRepository {
        public Task<MealInfo> GetMealById(int idMeal, int idTypeSnack, int idUser);
        public Task<SnacksByDay> GetSnacksByDate(DateTime snacks, int userId);
        public Task<Meal> AddMeal(Meal meal);
        public Task<Meal> UpdateMeal(Meal meal);
    }
}
