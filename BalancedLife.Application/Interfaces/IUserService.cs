using BalancedLife.Application.DTOs;

namespace BalancedLife.Application.interfaces {
    public interface IUserService {
        Task<UserInfoDTO> Login(string email, string password);
        Task<UserInfoDTO> Register(RegisterUserDTO user);
    }
}
