using BalancedLife.Application.DTOs.Snack;

namespace BalancedLife.Application.Interfaces {
    public interface ISnackService {
        Task<MealInfoDTO> GetMealById(int idMeal, int idTypeSnack, int idUser);
        Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId);
        Task<MealDTO> AddMeal(MealDTO snack);
        Task<MealDTO> Update(MealDTO snack);
        Task<SnackDTO> AddSnack(SnackDTO snack);
        Task<SnackDTO> UpdateSnack(SnackDTO snack);
        Task DeleteSnack(int id);

    }
}
