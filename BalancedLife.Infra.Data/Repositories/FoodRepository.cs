using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class FoodRepository : IFoodRepository {
        private readonly ApplicationDbContext _context;
        private readonly int _pageSize = 10;

        public FoodRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Food> Add(Food food) {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            return food;
        }

        public async Task<IEnumerable<Food>> FindFoodBySearch(string food, int pageNumber) {
            var foods = await _context.Foods
                .Where(f => f.Name.ToLower().Contains(food.ToLower()))
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();

            return foods;
        }

        public async Task<Food> GetFoodById(int id) {
            var food = await _context.Foods
                .Include(f => f.FoodNutritionInfos)
                    .ThenInclude(fni => fni.IdUnitMeasurementNavigation)
                .Include(f => f.FoodNutritionInfos)
                    .ThenInclude(fni => fni.IdNutritionalCompositionNavigation)
                .FirstOrDefaultAsync(f => f.Id == id);

            return food;
        }

    }
}
