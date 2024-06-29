using BalancedLife.Application.DTOs.Snack;

namespace BalancedLife.Application.Interfaces {
    public interface ISnackService {
        public Task<SnackDTO> GetSnackById(int id);
        public Task<SnacksByDayDTO> GetSnacksByDate(DateTime date, int userId);
        public Task<SnackDTO> Add(SnackDTO snack);
        public Task<SnackDTO> Update(SnackDTO snack);
    }
}
