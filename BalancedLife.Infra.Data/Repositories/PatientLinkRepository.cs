﻿using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using BalancedLife.Infra.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class PatientLinkRepository : IPatientRepository {
        private readonly ApplicationDbContext _context;

        public PatientLinkRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<UserPatientLink> AddPatient(UserPatientLink user) {
            await EntityHelper.LoadNavigationPropertyAsync(user, s => s.IdNutritionistNavigation, user.IdNutritionist, _context.UserInfos);
            await EntityHelper.LoadNavigationPropertyAsync(user, s => s.IdPatientNavigation, user.IdPatient, _context.UserInfos);

            var existingLink = await _context.UserPatientLinks
                .FirstOrDefaultAsync(up => up.IdNutritionist == user.IdNutritionist && up.IdPatient == user.IdPatient);

            if ( existingLink != null )
                throw new InvalidOperationException("O paciente já está vinculado a este nutricionista.");

            _context.UserPatientLinks.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeletePatient(long id, long idNutritionist) {
            var user = await _context.UserPatientLinks.FindAsync(id);

            if ( user == null )
                throw new KeyNotFoundException("O link paciente-nutricionista não foi encontrado.");

            if ( user.IdNutritionist != idNutritionist )
                throw new UnauthorizedAccessException("Você não tem permissão para deletar este paciente.");

            _context.UserPatientLinks.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserPatientLink> UpdatePatient(UserPatientLink user) {
            await EntityHelper.LoadNavigationPropertyAsync(user, s => s.IdNutritionistNavigation, user.IdNutritionist, _context.UserInfos);
            await EntityHelper.LoadNavigationPropertyAsync(user, s => s.IdPatientNavigation, user.IdPatient, _context.UserInfos);

            var existingUser = await _context.UserPatientLinks.FindAsync(user.Id);

            if ( existingUser == null )
                throw new KeyNotFoundException("O link paciente-nutricionista não foi encontrado.");

            if ( user.LinkStatus == 1 && user.IsCurrentNutritionist ) {
                var conflictingLink = await _context.UserPatientLinks
                    .Where(link => link.Id != user.Id &&
                                   link.IdPatient == user.IdPatient &&
                                   link.IsCurrentNutritionist)
                    .FirstOrDefaultAsync();

                if ( conflictingLink != null ) {
                    conflictingLink.IsCurrentNutritionist = false;
                }
            }

            existingUser.IdNutritionist = user.IdNutritionist;
            existingUser.IdPatient = user.IdPatient;
            existingUser.LinkStatus = user.LinkStatus;
            existingUser.IsCurrentNutritionist = user.IsCurrentNutritionist;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserPatientLink> GetPatientById(long id) {
            return await _context.UserPatientLinks.FirstOrDefaultAsync(up => up.Id == id);
        }

        public async Task<UserPatientLink> GetPatientByIdPatient(long id) {
            return await _context.UserPatientLinks.FirstOrDefaultAsync(up => up.IdPatient == id);
        }

        public async Task<IEnumerable<NutritionistLinkPatient>> GetNutritionistsByPatientId(long idPatient) {
            return await _context.UserPatientLinks
                .Include(up => up.IdNutritionistNavigation)
                    .ThenInclude(n => n.IdCityNavigation)
                        .ThenInclude(c => c.IdStateNavigation)
                .Where(up => up.IdPatient == idPatient)
                .Select(up => new NutritionistLinkPatient {
                    Link = up,
                    Nutritionist = up.IdNutritionistNavigation
                })
                .ToListAsync();
        }

        public async Task<UserInfo> GetActualNutritionist(long idPatient) {
            return await _context.UserPatientLinks
                .Include(up => up.IdNutritionistNavigation)
                .Where(up => up.IdPatient == idPatient && up.IsCurrentNutritionist)
                .Select(up => up.IdNutritionistNavigation)
                .FirstOrDefaultAsync();
        }
    }
}
