using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IBodyRepository {
        Task<Body> Add(Body body);
        Task<Body> GetBodyById(long id);
        Task<Body> Update(Body body);
        Task<IEnumerable<Body>> GetLastFourBodies(long userId);
    }
}
