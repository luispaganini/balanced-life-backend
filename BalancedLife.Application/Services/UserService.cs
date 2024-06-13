using AutoMapper;
using BalancedLife.Application.DTOs;
using BalancedLife.Application.interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Domain.Utils;

namespace BalancedLife.Application.Services {
    public class UserService : IUserService {
        private readonly IUserInfoRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserInfoRepository userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserInfoDTO> Login(string cpf, string password) {
            var user = await _userRepository.GetByCpf(cpf);
            if ( user == null || !UserInfoUtils.VerifyPassword(password, user.Password) )
                return null;

            return _mapper.Map<UserInfoDTO>(user);
        }

        public async Task<UserInfoDTO> Add(UserDTO user) {
            try {
                user.IsCompleteProfile = !string.IsNullOrEmpty(user.Password);
                var userInfo = _mapper.Map<UserInfo>(user);
                var addedUser = await _userRepository.Add(userInfo);
                return _mapper.Map<UserInfoDTO>(addedUser);
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while adding the user.", ex);
            }
        }

        public async Task<UserInfoDTO> Update(int id, UserDTO user) {
            try {
                user.IsCompleteProfile = !string.IsNullOrEmpty(user.Password);
                var userInfo = _mapper.Map<UserInfo>(user);
                userInfo.Id = id;
                var updatedUser = await _userRepository.Update(userInfo);
                return _mapper.Map<UserInfoDTO>(updatedUser);
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }

        public async Task<UserInfoDTO> GetUserById(int id) {
            try {
                var user = await _userRepository.GetById(id);
                return _mapper.Map<UserInfoDTO>(user);
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while retrieving the user by ID.", ex);
            }
        }

        public async Task<UserInfoDTO> VerifyCPF(string cpf) {
            try {
                var user = await _userRepository.GetByCpf(cpf);
                return user != null ? _mapper.Map<UserInfoDTO>(user) : null;
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while verifying the CPF.", ex);
            }
        }
    }
}
