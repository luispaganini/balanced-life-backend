using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IUserInfoRepository {
        Task<UserInfo> Add(UserInfo user);
        Task<UserInfo> GetByEmail(string email);
        Task<UserInfo> GetByCpf(string cpf);
        Task<UserInfo> GetById(long id);
        Task<UserInfo> Update(UserInfo user);
        Task<IEnumerable<Patient>> GetPatients(long id);
    }
}
