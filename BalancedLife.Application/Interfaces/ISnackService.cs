using BalancedLife.Application.DTOs.Snack;

namespace BalancedLife.Application.Interfaces {
    public interface ISnackService {
        Task<MealInfoDTO> GetMealById(int idMeal, int idTypeSnack, int idUser);
        Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId);
        Task<MealDTO> AddMeal(MealDTO snack);
        Task<MealDTO> UpdateMeal(MealDTO snack);
        Task UpdateMealStatus(long idMeal, MealStatusDTO mealStatus);
        Task<SnackDTO> AddSnack(SnackFullDTO snack);
        Task<SnackDTO> UpdateSnack(SnackFullDTO snack);
        Task DeleteSnack(long id);

    }
}
