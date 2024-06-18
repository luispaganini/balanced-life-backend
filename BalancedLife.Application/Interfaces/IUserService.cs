using BalancedLife.Application.DTOs;

namespace BalancedLife.Application.interfaces {
    public interface IUserService {
        Task<UserInfoDTO> Add(UserDTO user);
        Task<UserInfoDTO> Update(long id, UserDTO user);
        Task<UserInfoDTO> GetUserById(int id);
    }
}
