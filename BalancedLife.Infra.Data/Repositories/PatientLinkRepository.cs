using BalancedLife.Domain.Entities;
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

            if (user == null ) 
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
            

            existingUser.IdNutritionist = user.IdNutritionist;
            existingUser.IdPatient = user.IdPatient;
            existingUser.LinkStatus = user.LinkStatus;
            existingUser.IsCurrentNutritionist = user.IsCurrentNutritionist;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
