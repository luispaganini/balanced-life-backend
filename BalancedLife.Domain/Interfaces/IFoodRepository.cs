﻿using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IFoodRepository {
        Task<Food> Add(Food food);
        Task<IEnumerable<Food>> FindFoodBySearch(string food, int pageNumber, int pageSize);
        Task<Food> GetFoodById(int id);
        Task<IEnumerable<NutritionalComposition>> GetNutritionalCompositions();
        Task<IEnumerable<UnitMeasurement>> GetUnitsMeasurement();
    }
}
