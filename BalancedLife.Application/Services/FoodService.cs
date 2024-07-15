﻿using AutoMapper;
using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.Services {
    public class FoodService : IFoodService {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public FoodService(IFoodRepository foodRepository, IMapper mapper) {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<FoodDTO> Add(FoodDTO food) {
            try {
                var foodEntity = _mapper.Map<Food>(food);
                var addedFood = await _foodRepository.Add(foodEntity);

                return _mapper.Map<FoodDTO>(addedFood);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while adding the food.", ex);
            }
        }

        public async Task<IEnumerable<FoodDTO>> FindFoodBySearch(string food, int pageNumber) {
            try {
                var foods = await _foodRepository.FindFoodBySearch(food, pageNumber);
                return _mapper.Map<IEnumerable<FoodDTO>>(foods);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while searching for food.", ex);
            }
        }
    }
}
