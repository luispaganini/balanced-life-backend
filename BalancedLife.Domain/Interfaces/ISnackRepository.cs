using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface ISnackRepository {
        Task<MealInfo> GetMealById(int idMeal, int idTypeSnack, int idUser);
        Task<SnacksByDay> GetSnacksByDate(DateTime snacks, int userId);
        Task<Meal> AddMeal(Meal meal);
        Task<Meal> UpdateMeal(Meal meal);
        Task<Snack> AddSnack(Snack snack);
        Task<Snack> UpdateSnack(Snack snack);
        Task DeleteSnack(int id);
    }
}
