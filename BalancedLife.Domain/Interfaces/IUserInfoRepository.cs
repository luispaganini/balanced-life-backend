using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Enums;

namespace BalancedLife.Domain.Interfaces {
    public interface IUserInfoRepository {
        Task<UserInfo> Add(UserInfo user);
        Task<UserInfo> GetByEmail(string email);
        Task<UserInfo> GetByCpf(string cpf);
        Task<UserInfo> GetById(long id);
        Task<UserInfo> Update(UserInfo user);
        Task<IEnumerable<Patient>> GetPatients(long id, int pageNumber, int pageSize, string? patientName, StatusNutritionist? status);
        Task<int> GetNumberPagePatients(long id, int pageSize, string? patientName, StatusNutritionist? status);
    }
}
