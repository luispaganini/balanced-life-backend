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

        public async Task<IEnumerable<PatientDTO>> GetPatients(long idNutritionist) {
            var patients = await _userRepository.GetPatients(idNutritionist);
            return _mapper.Map<IEnumerable<PatientDTO>>(patients);
        }

        public async Task<PatientLinkDTO> UpdatePatient(PatientLinkDTO user) {
            var patient = _mapper.Map<UserPatientLink>(user);
            var result = await _patientRepository.UpdatePatient(patient);
            return _mapper.Map<PatientLinkDTO>(result);
        }

        public async Task<bool> IsYourPatient(long idNutritionist, long idUserLink) {
            var patient = await _patientRepository.GetPatientById(idUserLink);

            if (patient == null)
                return false;

            return (patient.IdNutritionist == idNutritionist) && (patient.LinkStatus == (int)StatusNutritionist.Accepted);
        }
    }
}
