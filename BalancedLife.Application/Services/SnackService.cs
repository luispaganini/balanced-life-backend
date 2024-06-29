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

        public Task<SnackDTO> Add(SnackDTO snack) {
            throw new NotImplementedException();
        }

        public Task<SnackDTO> GetSnackById(int id) {
            throw new NotImplementedException();
        }

        public async Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId) {
            var result = await _snackRepository.GetSnacksByDate(date, userId);

            return _mapper.Map<SnacksByDayDTO>(result);

        }

        public Task<SnackDTO> Update(SnackDTO snack) {
            throw new NotImplementedException();
        }
    }
}
