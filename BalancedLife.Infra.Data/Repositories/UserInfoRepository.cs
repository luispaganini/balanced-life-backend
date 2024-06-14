using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class UserInfoRepository : IUserInfoRepository {
        private readonly ApplicationDbContext _context;

        public UserInfoRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<UserInfo> Add(UserInfo user) {
            _context.UserInfos.Add(user);
            await _context.SaveChangesAsync();

            // Recarregar o usuário com suas propriedades de navegação incluídas
            return await _context.UserInfos
                .Include(u => u.IdUserRoleNavigation)
                .Include(u => u.IdCityNavigation)
                    .ThenInclude(c => c.IdStateNavigation)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        public async Task<UserInfo> GetByCpf(string cpf) {
            return await _context.UserInfos
                .Include(u => u.IdUserRoleNavigation)
                .Include(u => u.IdCityNavigation)
                    .ThenInclude(c => c.IdStateNavigation)
                .FirstOrDefaultAsync(u => u.Cpf == cpf);
        }

        public async Task<UserInfo> GetByEmail(string email) {
            return await _context.UserInfos
                .Include(u => u.IdUserRoleNavigation)
                .Include(u => u.IdCityNavigation)
                    .ThenInclude(c => c.IdStateNavigation)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserInfo> GetById(long id) {
            var userinfo = await _context.UserInfos
                .Include(u => u.IdUserRoleNavigation)
                .Include(u => u.IdCityNavigation)
                    .ThenInclude(c => c.IdStateNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);

            return userinfo;
        }

        public async Task<UserInfo> Update(UserInfo user) {
            _context.UserInfos.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
