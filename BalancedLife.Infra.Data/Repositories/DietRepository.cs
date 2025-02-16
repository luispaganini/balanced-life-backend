using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class DietRepository : IDietRepository {
        private readonly ApplicationDbContext _context;

        public DietRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Diet> AddDiet(Diet diet) {
            bool exists = await _context.Diets
                .AnyAsync(d => d.IdPatient == diet.IdPatient &&
                               d.DietDays.Any(dd => diet.DietDays.Select(x => x.IdDay).Contains(dd.IdDay)));

            if ( exists ) {
                throw new InvalidOperationException("Já existe uma dieta cadastrada para um ou mais dias selecionados.");
            }

            diet.CreatedAt = DateTime.Now;

            _context.Diets.Add(diet);
            await _context.SaveChangesAsync();

            return diet;
        }

        public async Task<Diet> GetDietById(long id) {
            return await _context.Diets
                .Include(d => d.DietDays)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Diet>> GetDietsByDay(DateTime date, long idPatient) {
            int dayOfWeekId = (int) date.DayOfWeek;
            dayOfWeekId = dayOfWeekId == 0 ? 7 : dayOfWeekId;

            return await _context.Diets
                .Include(d => d.DietDays)
                .Where(d => d.IdPatient == idPatient && d.DietDays.Any(dd => dd.IdDay == dayOfWeekId))
                .Include(d => d.Meals)
                .ToListAsync();
        }

        public async Task<Diet> UpdateDiet(Diet diet) {
            var existingDiet = await _context.Diets
                .Include(d => d.DietDays)
                .FirstOrDefaultAsync(d => d.Id == diet.Id);

            if ( existingDiet == null ) {
                throw new KeyNotFoundException("Dieta não encontrada.");
            }

            bool exists = await _context.Diets
                .AnyAsync(d => d.Id != diet.Id &&
                               d.IdPatient == diet.IdPatient &&
                               d.DietDays.Any(dd => diet.DietDays.Select(x => x.IdDay).Contains(dd.IdDay)));

            if ( exists ) {
                throw new InvalidOperationException("Já existe uma dieta cadastrada para um ou mais dias selecionados.");
            }

            _context.Entry(existingDiet).CurrentValues.SetValues(diet);

            existingDiet = diet;

            await _context.SaveChangesAsync();
            return existingDiet;
        }

        public async Task<bool> DeleteDiet(long id) {
            var diet = await _context.Diets
                .Include(d => d.DietDays)
                .Include(d => d.Meals)
                .FirstOrDefaultAsync(d => d.Id == id);

            if ( diet == null ) {
                return false;
            }

            _context.DietDays.RemoveRange(diet.DietDays);
            _context.Meals.RemoveRange(diet.Meals);
            _context.Diets.Remove(diet);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Diet>> GetDietsByPatient(long id, long idNutritionist) {
            var existingLink = await _context.UserPatientLinks
                .FirstOrDefaultAsync(up => up.IdNutritionist == idNutritionist && up.IdPatient == id);

            if ( existingLink == null ) 
                throw new UnauthorizedAccessException("Você não tem permissão para acessar as dietas deste paciente.");

            var diets = await _context.Diets
                .Include(d => d.DietDays)
                .Include(d => d.Meals)
                .Where(d => d.IdPatient == id && d.IdNutritionist == idNutritionist)
                .ToListAsync();

            return diets;
        }
    }
}
