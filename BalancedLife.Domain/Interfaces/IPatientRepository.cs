using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IPatientRepository {
        Task<UserPatientLink> AddPatient(UserPatientLink user);
        Task<UserPatientLink> UpdatePatient(UserPatientLink user);
        Task DeletePatient(long id, long idNutritionist);
        Task<UserPatientLink> GetPatientById(long id);
        Task<UserPatientLink> GetPatientByIdPatient(long id);
        Task<IEnumerable<NutritionistLinkPatient>> GetNutritionistsByPatientId(long idPatient);
        Task<UserInfo> GetActualNutritionist(long idPatient);

    }
}
