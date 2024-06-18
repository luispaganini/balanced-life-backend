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

        public async Task<UserInfoDTO> Add(UserDTO user) {
            if ( user == null ) {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            try {
                user.IsCompleteProfile = !string.IsNullOrEmpty(user.Password);
                user.Password = UserInfoUtils.HashPassword(user.Password);
                var userInfo = _mapper.Map<UserInfo>(user);

                if ( await UserExistsByCpfOrEmail(user.Cpf, user.Email) ) 
                    throw new ApplicationException("User already exists.");

                var addedUser = await _userRepository.Add(userInfo);
                return _mapper.Map<UserInfoDTO>(addedUser);
            } catch ( ApplicationException ) {
                throw; 
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while adding the user.", ex);
            }
        }

        public async Task<UserInfoDTO> Update(long id, UserDTO user) {
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

        private async Task<bool> UserExistsByCpfOrEmail(string cpf, string email) {
            var userByCpf = await _userRepository.GetByCpf(cpf);
            if ( userByCpf != null ) {
                return true;
            }

            var userByEmail = await _userRepository.GetByEmail(email);
            if ( userByEmail != null ) {
                return true;
            }

            return false;
        }
    }
}
