using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IUserInfoRepository {
        Task<UserInfo> Add(UserInfo user);
        Task<UserInfo> GetByEmail(string email);
        Task<UserInfo> GetById(long id);
        Task<UserInfo> Update(UserInfo user);
    }
}
