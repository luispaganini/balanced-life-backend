﻿
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Enums;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using BalancedLife.Infra.Data.Utils;
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

        public async Task<IEnumerable<Patient>> GetPatients(long id, int pageNumber, int pageSize, string? patientName, StatusNutritionist? status) {
            var patients = await (from up in _context.UserPatientLinks
                                  join us in _context.UserInfos on up.IdPatient equals us.Id
                                  where up.IdNutritionist == id
                                  && (patientName == null || us.Name.Contains(patientName))
                                  && (status == null || up.LinkStatus == (int) status)
                                  select new Patient {
                                      Id = up.Id,
                                      Name = us.Name,
                                      IsCurrentNutritionist = up.IsCurrentNutritionist,
                                      LinkStatus = (StatusNutritionist) up.LinkStatus,
                                      Age = EntityHelper.CalculateAge(us.Birth),
                                  })
                                    .OrderByDescending(e => e.LinkStatus)
                                    .ThenBy(p => p.Name)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return patients;
        }

        public async Task<UserInfo> Update(UserInfo user) {
            _context.UserInfos.Update(user);
            await _context.SaveChangesAsync();

            UserInfo userInfo = await _context.UserInfos
                .Include(u => u.IdUserRoleNavigation)
                .Include(u => u.IdCityNavigation)
                    .ThenInclude(c => c.IdStateNavigation)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            return userInfo;
        }

    }
}
