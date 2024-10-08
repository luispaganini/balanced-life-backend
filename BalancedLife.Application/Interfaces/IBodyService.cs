using BalancedLife.Application.DTOs.Body;

namespace BalancedLife.Application.Interfaces {
    public interface IBodyService {
        Task<BodyDTO> Add(BodyDTO body);
        Task<BodyDTO> GetBodyById(long id);
        Task<BodyDTO> Update(BodyDTO body);
        Task<IEnumerable<BodyDTO>> GetLastFourBodies(long userId);
    }
}
