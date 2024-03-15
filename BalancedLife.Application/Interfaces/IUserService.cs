using BalancedLife.Application.DTOs;

namespace BalancedLife.Application.interfaces {
    public interface IUserService {
        Task<UserInfoDTO> Login(string cpf, string password);
        Task<UserInfoDTO> Add(UserDTO user);
        Task<UserInfoDTO> Update(int id, UserDTO user);
        Task<UserInfoDTO> GetUserById(int id);
    }
}
