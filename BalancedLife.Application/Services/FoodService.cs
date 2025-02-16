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
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<FoodSearchDTO> FindFoodBySearch(string food, int pageNumber, int pageSize, List<int> tables) {
            try {
                var (foods, totalPages) = await _foodRepository.FindFoodBySearch(food, pageNumber, pageSize, tables);

                var foodsDTO = _mapper.Map<IEnumerable<FoodDTO>>(foods);

                return new FoodSearchDTO {
                    Foods = foodsDTO,
                    QuantityPages = totalPages
                };
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while searching for food.", ex);
            }
        }

        public async Task<FoodDTO> GetFoodById(int id) {
            try {
                var food = await _foodRepository.GetFoodById(id);
                return _mapper.Map<FoodDTO>(food);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while getting the food.", ex);
            }
        }

        public async Task<IEnumerable<NutritionalCompositionDTO>> GetNutritionalCompositions() {
            try {
                var nutritionalCompositions = await _foodRepository.GetNutritionalCompositions();
                return _mapper.Map<IEnumerable<NutritionalCompositionDTO>>(nutritionalCompositions);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while getting the nutritional compositions.", ex);
            }
        }

        public async Task<IEnumerable<UnitMeasurementDTO>> GetUnitsMeasurement() {
            try {
                var unitsMeasurement = await _foodRepository.GetUnitsMeasurement();
                return _mapper.Map<IEnumerable<UnitMeasurementDTO>>(unitsMeasurement);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while getting the units of measurement.", ex);
            }
        }
    }
}
