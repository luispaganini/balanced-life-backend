using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IFoodRepository {
        Task<Food> Add(Food food);
        Task<IEnumerable<Food>> FindFoodBySearch(string food, int pageNumber);
        Task<Food> GetFoodById(int id);
    }
}
