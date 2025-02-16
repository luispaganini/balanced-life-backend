using BalancedLife.Application.DTOs.Diet;

namespace BalancedLife.Application.Interfaces {
    public interface IDietService {
        Task<DietDTO> AddDiet(DietDTO diet);
        Task<DietDTO> GetDietById(long id);
        Task<DietDTO> UpdateDiet(DietDTO diet);
        Task<IEnumerable<DietDTO>> GetDietsByDay(DateTime date, long idPatient);
        Task<bool> DeleteDiet(long id);
        Task<IEnumerable<DietDTO>> GetDietsByPatient(long id, long idNutritionist);
    }
}
