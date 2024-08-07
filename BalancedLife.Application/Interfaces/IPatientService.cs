using BalancedLife.Application.DTOs.User;

namespace BalancedLife.Application.Interfaces {
    public interface IPatientService {
        Task<IEnumerable<PatientLinkDTO>> GetPatients(long id);
        Task DeletePatient(long id, long idNutritionist);
        Task<PatientLinkDTO> AddPatient(PatientLinkDTO user);
        Task<PatientLinkDTO> UpdatePatient(PatientLinkDTO user);

    }
}
