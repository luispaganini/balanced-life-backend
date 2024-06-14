using AutoMapper;
using BalancedLife.Application.DTOs;
using BalancedLife.Application.interfaces;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Domain.Utils;
using System.Security.Claims;
using System.Text;

namespace BalancedLife.Application.Services {
    public class LoginService : ILoginService {
        private readonly IUserInfoRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IRefreshTokenRepository _refreshRepository;
        private readonly IMapper _mapper;
        public LoginService(IUserInfoRepository userRepository, IRefreshTokenRepository refreshRepository, IMapper mapper, IUserService userService) {
            _userRepository = userRepository;
            _refreshRepository = refreshRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<UserInfoDTO> Login(string cpf, string password) {
            var user = await _userRepository.GetByCpf(cpf);
            if ( user == null || !UserInfoUtils.VerifyPassword(password, user.Password) )
                return null;

            return _mapper.Map<UserInfoDTO>(user);
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

        public async Task AddRefreshToken(RefreshToken refreshToken) {
            try {
                await _refreshRepository.AddAsync(refreshToken);
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while refreshing the token.", ex);
            }
        }

        public async Task<RefreshToken> GetByTokenAsync(string token) {
            try {
                return await _refreshRepository.GetByTokenAsync(token);
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while getting the token.", ex);
            }
        } 
        
        public async Task UpdateTokenAsync(RefreshToken refreshToken) {
            try {
                await _refreshRepository.UpdateAsync(refreshToken);
            } catch ( Exception ex ) {
                // Log exception
                throw new ApplicationException("An error occurred while updating the token.", ex);
            }
        }
    }
}
