using AutoMapper;
using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.Services {
    public class SnackService : ISnackService {
        private readonly ISnackRepository _snackRepository;
        private readonly IMapper _mapper;

        public SnackService(ISnackRepository snackRepository, IMapper mapper) {
            _snackRepository = snackRepository;
            _mapper = mapper;
        }

        public async Task<MealDTO> AddMeal(MealDTO snack) {
            var snackEntity = _mapper.Map<Meal>(snack);
            var addedSnack = await _snackRepository.AddMeal(snackEntity);

            return _mapper.Map<MealDTO>(addedSnack);
        }

        public async Task<SnackDTO> AddSnack(SnackFullDTO snack) {
            var snackEntity = _mapper.Map<Snack>(snack);
            var addedSnack = await _snackRepository.AddSnack(snackEntity);

            return _mapper.Map<SnackDTO>(addedSnack);
        }

        public async Task DeleteSnack(long id) {
            await _snackRepository.DeleteSnack(id);
        }

        public async Task<MealInfoDTO> GetMealById(int idMeal, int idTypeSnack, int idUser) {
            var result = await _snackRepository.GetMealById(idMeal, idTypeSnack, idUser);

            return _mapper.Map<MealInfoDTO>(result);
        }

        public async Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId) {
            var result = await _snackRepository.GetSnacksByDate(date, userId);

            return _mapper.Map<SnacksByDayDTO>(result);

        }

        public async Task<MealDTO> UpdateMeal(MealDTO snack) {
            var snackEntity = _mapper.Map<Meal>(snack);
            var updatedSnack = await _snackRepository.UpdateMeal(snackEntity);

            return _mapper.Map<MealDTO>(updatedSnack);
        }

        public async Task<SnackDTO> UpdateSnack(SnackFullDTO snack) {
            var snackEntity = _mapper.Map<Snack>(snack);
            var updatedSnack = await _snackRepository.UpdateSnack(snackEntity);

            return _mapper.Map<SnackDTO>(updatedSnack);
        }
    }
}
