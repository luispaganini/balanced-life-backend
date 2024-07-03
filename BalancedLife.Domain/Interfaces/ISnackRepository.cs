﻿using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface ISnackRepository {
        public Task<Meal> GetMealById(int id);
        public Task<SnacksByDay> GetSnacksByDate(DateTime snacks, int userId);
        public Task<object> Add(object snack);
        public Task<object> Update(object snack);
    }
}
