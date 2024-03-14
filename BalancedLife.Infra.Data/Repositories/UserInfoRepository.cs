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
            return user;
        }

        public async Task<UserInfo> GetByEmail(string email) {
            return await _context.UserInfos.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserInfo> GetById(long id) {
            return await _context.UserInfos.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserInfo> Update(UserInfo user) {
            _context.UserInfos.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
