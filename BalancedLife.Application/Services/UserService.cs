using AutoMapper;
using BalancedLife.Application.DTOs;
using BalancedLife.Application.interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.services {
    public class UserService : IUserService {
        private readonly IUserInfoRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserInfoRepository userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserInfoDTO> Login(string cpf, string password) {
            var user = await _userRepository.GetByCpf(cpf);

            if ( user != null && user.VerifyPassword(password, user.Password) )
                return _mapper.Map<UserInfoDTO>(user);

            return null;
        }

        public async Task<UserInfoDTO> Add(UserDTO user) {
            user.IsCompleteProfile = (user.Password != null);

            return _mapper.Map<UserInfoDTO>(await _userRepository.Add(_mapper.Map<UserInfo>(user)));
        }

        public async Task<UserInfoDTO> Update(int id, UserDTO user) {
            user.IsCompleteProfile = (user.Password != null);

            var userInfo = _mapper.Map<UserInfo>(user);
            userInfo.Id = id;

            return _mapper.Map<UserInfoDTO>(await _userRepository.Update(userInfo));
        }
    }
}
