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

        public async Task<UserInfoDTO> Login(string email, string password) {
            var user = await _userRepository.GetByEmail(email);

            if ( user.VerifyPassword(password, user.Password) )
                return _mapper.Map<UserInfoDTO>(user);

            return null;
        }

        public async Task<UserInfoDTO> Register(RegisterUserDTO user) {
            var userRegister = await _userRepository.Add(_mapper.Map<UserInfo>(user));
            return _mapper.Map<UserInfoDTO>(userRegister);
        }
    }
}
