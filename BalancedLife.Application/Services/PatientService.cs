using AutoMapper;
using BalancedLife.Application.DTOs.User;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Enums;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.Services {
    public class PatientService : IPatientService {
        private readonly IUserInfoRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IUserInfoRepository userRepository, IMapper mapper, IPatientRepository patientRepository) {
            _userRepository = userRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
        }

        public async Task<PatientLinkDTO> AddPatient(PatientLinkDTO user) {
            var patient = _mapper.Map<UserPatientLink>(user);
            var result = await _patientRepository.AddPatient(patient);
            return _mapper.Map<PatientLinkDTO>(result);
        }

        public async Task DeletePatient(long id, long idNutritionist) {
            await _patientRepository.DeletePatient(id, idNutritionist);
        }

        public async Task<IEnumerable<PatientDTO>> GetPatients(long idNutritionist, int pageNumber, int pageSize, string? patientName, StatusNutritionist? status) {
            var patients = await _userRepository.GetPatients(idNutritionist, pageNumber, pageSize, patientName, status);
            return _mapper.Map<IEnumerable<PatientDTO>>(patients);
        }

        public async Task<int> GetNumberPagePatients(long id, int pageSize, string? patientName, StatusNutritionist? status) {
            var numberPage = await _userRepository.GetNumberPagePatients(id, pageSize, patientName, status);

            return numberPage;
        }

        public async Task<PatientLinkDTO> GetPatientLinkById(long id) {
            var patient = await _patientRepository.GetPatientById(id);
            return _mapper.Map<PatientLinkDTO>(patient);
        }

        public async Task<PatientLinkDTO> UpdatePatient(PatientLinkDTO user) {
            var patient = _mapper.Map<UserPatientLink>(user);
            var result = await _patientRepository.UpdatePatient(patient);
            return _mapper.Map<PatientLinkDTO>(result);
        }

        public async Task<PatientVerifyDTO> IsYourPatient(long idNutritionist, long idUserLink) {
            var patient = await _patientRepository.GetPatientById(idUserLink);

            if ( patient == null )
                return new PatientVerifyDTO {
                    IsPatient = false,
                    IdPatient = 0
                };

            return new PatientVerifyDTO {
                IsPatient = (patient.IdNutritionist == idNutritionist) && (patient.LinkStatus == (int) StatusNutritionist.Accepted),
                IdPatient = patient.IdPatient
            };
        }

        public async Task<PatientVerifyDTO> IsYourPatientByPatientId(long idNutritionist, long idPatient) {
            var patient = await _patientRepository.GetPatientByIdPatient(idPatient);

            if ( patient == null )
                return new PatientVerifyDTO {
                    IsPatient = false,
                    IdPatient = 0
                };

            return new PatientVerifyDTO {
                IsPatient = (patient.IdNutritionist == idNutritionist) && (patient.LinkStatus == (int) StatusNutritionist.Accepted),
                IdPatient = patient.IdPatient
            };
        }

        public async Task<IEnumerable<NutritionistLinkPatientDTO>> GetNutritionistsByPatientId(long idPatient) {
            var patient = await _patientRepository.GetNutritionistsByPatientId(idPatient);
            return _mapper.Map<IEnumerable<NutritionistLinkPatientDTO>>(patient);
        }

        public async Task<UserInfoDTO> GetActualNutritionist(long idPatient) {
            var nutritionist = await _patientRepository.GetActualNutritionist(idPatient);
            return _mapper.Map<UserInfoDTO>(nutritionist);
        }
    }
}
