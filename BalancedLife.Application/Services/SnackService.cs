using AutoMapper;
using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.Services {
    public class SnackService : ISnackService {
        private readonly ISnackRepository _snackRepository;
        private readonly IMapper _mapper;

        public SnackService(ISnackRepository snackRepository, IMapper mapper) {
            _snackRepository = snackRepository;
            _mapper = mapper;
        }

        public Task<MealDTO> Add(MealDTO snack) {
            throw new NotImplementedException();
        }

        public async Task<MealInfoDTO> GetMealById(int idMeal, int idTypeSnack, int idUser) {
            var result = await _snackRepository.GetMealById(idMeal, idTypeSnack, idUser);

            return _mapper.Map<MealInfoDTO>(result);
        }

        public async Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId) {
            var result = await _snackRepository.GetSnacksByDate(date, userId);

            return _mapper.Map<SnacksByDayDTO>(result);

        }

        public Task<MealDTO> Update(MealDTO snack) {
            throw new NotImplementedException();
        }
    }
}
