using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IDietRepository {
        Task<Diet> AddDiet(Diet diet);
        Task<Diet> GetDietById(long id);
        Task<Diet> UpdateDiet(Diet diet);
        Task<IEnumerable<Diet>> GetDietsByDay(DateTime date, long idPatient);
        Task<bool> DeleteDiet(long id);
        Task<IEnumerable<Diet>> GetDietsByPatient(long id, long idNutritionist);

    }
}
