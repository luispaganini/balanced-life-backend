using AutoMapper;
using BalancedLife.Application.DTOs.Body;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.Services {
    public class BodyService : IBodyService {
        private readonly IBodyRepository _bodyRepository;
        private readonly IMapper _mapper;

        public BodyService(IBodyRepository bodyRepository, IMapper mapper) {
            _bodyRepository = bodyRepository;
            _mapper = mapper;
        }

        public async Task<BodyDTO> Add(BodyDTO body) {
            if ( body == null )
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");

            try {
                var bodyEntity = _mapper.Map<Body>(body);
                var addedBody = await _bodyRepository.Add(bodyEntity);
                return _mapper.Map<BodyDTO>(addedBody);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while adding the body.", ex);
            }
        }

        public async Task<BodyDTO> GetBodyById(long id) {
            try {
                var body = await _bodyRepository.GetBodyById(id);
                return _mapper.Map<BodyDTO>(body);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while retrieving the body by ID.", ex);
            }
        }

        public async Task<IEnumerable<BodyDTO>> GetLastFourBodies(long userId) {
            try {
                var bodies = await _bodyRepository.GetLastFourBodies(userId);
                return _mapper.Map<IEnumerable<BodyDTO>>(bodies);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while retrieving the last four bodies.", ex);
            }
        }

        public async Task<BodyDTO> Update(BodyDTO body) {
            try {
                var bodyEntity = _mapper.Map<Body>(body);
                var updatedBody = await _bodyRepository.Update(bodyEntity);
                return _mapper.Map<BodyDTO>(updatedBody);
            } catch ( Exception ex ) {
                throw new ApplicationException("An error occurred while updating the body.", ex);
            }
        }
    }
}
