using AutoMapper;
using BalancedLife.Application.DTOs.User;
using BalancedLife.Application.interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Domain.Utils;
using System.Text.Json;

namespace BalancedLife.Application.Services
{
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
                    throw new ApplicationException("Já existe um usuário cadastrado com esse CPF ou e-mail.");

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
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }

        public async Task<UserInfoDTO> GetUserById(long id) {
            try {
                var user = await _userRepository.GetById(id);
                return _mapper.Map<UserInfoDTO>(user);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while retrieving the user by ID.", ex);
            }
        }

        public async Task<UserInfoDTO> PatchUpdate(long id, Dictionary<string, object> updates) {
            var user = await _userRepository.GetById(id);
            if ( user == null ) 
                throw new Exception("Usuário não existente");

            foreach ( var update in updates ) {
                var property = typeof(UserInfo)
                    .GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, update.Key, StringComparison.OrdinalIgnoreCase));

                if ( property != null && property.CanWrite ) {
                    if ( string.Equals(property.Name, "Password", StringComparison.OrdinalIgnoreCase) ) {
                        var newPassword = update.Value.ToString();
                        var hashedPassword = UserInfoUtils.HashPassword(newPassword);
                        property.SetValue(user, hashedPassword);
                    } else {
                        var jsonValue = JsonSerializer.Serialize(update.Value);
                        var value = JsonSerializer.Deserialize(jsonValue, property.PropertyType);
                        property.SetValue(user, value);
                    }
                }
            }

            var userInfo = await _userRepository.Update(user);
            return _mapper.Map<UserInfoDTO>(userInfo);
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
