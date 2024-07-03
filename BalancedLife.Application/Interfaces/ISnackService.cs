using BalancedLife.Application.DTOs.Snack;

namespace BalancedLife.Application.Interfaces {
    public interface ISnackService {
        public Task<MealDTO> GetMealById(int id);
        public Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId);
        public Task<MealDTO> Add(MealDTO snack);
        public Task<MealDTO> Update(MealDTO snack);
    }
}
