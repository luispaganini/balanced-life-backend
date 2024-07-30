using BalancedLife.Application.DTOs;

namespace BalancedLife.Application.interfaces {
    public interface IUserService {
        Task<UserInfoDTO> Add(UserDTO user);
        Task<UserInfoDTO> Update(long id, UserDTO user);
        Task<UserInfoDTO> GetUserById(long id);
        Task<UserInfoDTO> PatchUpdate(long id, Dictionary<string, object> updates);
    }
}
