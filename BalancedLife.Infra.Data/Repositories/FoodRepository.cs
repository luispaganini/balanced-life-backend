﻿using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using BalancedLife.Infra.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class FoodRepository : IFoodRepository {
        private readonly ApplicationDbContext _context;
        private readonly int _pageSize = 10;

        public FoodRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Food> Add(Food food) {
            var existingFood = await _context.Foods
                .FirstOrDefaultAsync(f => f.Name == food.Name);

            if ( existingFood != null ) 
                throw new Exception($"Um alimento com o nome '{food.Name}' já existe.");
            

            await EntityHelper.LoadNavigationPropertyAsync(food, s => s.IdFoodGroupNavigation, food.IdFoodGroup, _context.FoodGroups);

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            foreach ( var nutritionInfo in food.FoodNutritionInfos ) {
                await EntityHelper.LoadNavigationPropertyAsync(nutritionInfo, s => s.IdNutritionalCompositionNavigation, nutritionInfo.IdNutritionalComposition, _context.NutritionalCompositions);
                await EntityHelper.LoadNavigationPropertyAsync(nutritionInfo, s => s.IdUnitMeasurementNavigation, nutritionInfo.IdUnitMeasurement, _context.UnitMeasurements);
                nutritionInfo.IdFood = food.Id;
            }

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

        public async Task<IEnumerable<NutritionalComposition>> GetNutritionalCompositions() {
            var nutritionalCompositions = await _context.NutritionalCompositions.ToListAsync();

            return nutritionalCompositions;
        }

        public async Task<IEnumerable<UnitMeasurement>> GetUnitsMeasurement() {
            var unitsMeasurement = await _context.UnitMeasurements.ToListAsync();

            return unitsMeasurement;
        }
    }
}
