using BalancedLife.Application.DTOs.User;

namespace BalancedLife.Application.Interfaces {
    public interface IPatientService {
        Task<IEnumerable<PatientDTO>> GetPatients(long id);
        Task DeletePatient(long id, long idNutritionist);
        Task<PatientLinkDTO> AddPatient(PatientLinkDTO user);
        Task<PatientLinkDTO> UpdatePatient(PatientLinkDTO user);
        Task<PatientLinkDTO> GetPatientLinkById(long id);
        Task<PatientVerifyDTO> IsYourPatient(long idNutritionist, long idPatient);
        Task<PatientVerifyDTO> IsYourPatientByPatientId(long idNutritionist, long idPatient);

    }
}
