using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class BodyRepository : IBodyRepository {
        private readonly ApplicationDbContext _context;

        public BodyRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Body> Add(Body body) {
            _context.Bodies.Add(body);
            await _context.SaveChangesAsync();

            return body;
        }

        public async Task<Body> GetBodyById(long id) {
            return await _context.Bodies
                .Include(b => b.IdUserNavigation)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Body>> GetLastFourBodies(long userId) {
            return await _context.Bodies
                .Include(b => b.IdUserNavigation)
                .Where(b => b.IdUser == userId)
                .OrderByDescending(b => b.Datetime)
                .Take(4)
                .ToListAsync();
        }

        public async Task<Body> Update(Body body) {
            _context.Bodies.Update(body);
            await _context.SaveChangesAsync();
            return body;
        }
    }
}
