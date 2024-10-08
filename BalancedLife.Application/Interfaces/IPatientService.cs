using BalancedLife.Application.DTOs.User;
using BalancedLife.Domain.Enums;

namespace BalancedLife.Application.Interfaces {
    public interface IPatientService {
        Task<IEnumerable<PatientDTO>> GetPatients(long id, int pageNumber, int pageSize, string? patientName, StatusNutritionist? status);
        Task DeletePatient(long id, long idNutritionist);
        Task<PatientLinkDTO> AddPatient(PatientLinkDTO user);
        Task<PatientLinkDTO> UpdatePatient(PatientLinkDTO user);
        Task<PatientLinkDTO> GetPatientLinkById(long id);
        Task<PatientVerifyDTO> IsYourPatient(long idNutritionist, long idPatient);
        Task<PatientVerifyDTO> IsYourPatientByPatientId(long idNutritionist, long idPatient);
        Task<IEnumerable<NutritionistLinkPatientDTO>> GetNutritionistsByPatientId(long idPatient);
        Task<UserInfoDTO> GetActualNutritionist(long idPatient);

    }
}
