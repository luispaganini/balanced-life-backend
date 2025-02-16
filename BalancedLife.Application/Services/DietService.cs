using AutoMapper;
using BalancedLife.Application.DTOs.Diet;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;

namespace BalancedLife.Application.Services {
    public class DietService : IDietService {
        private readonly IDietRepository _dietRepository;
        private readonly IMapper _mapper;

        public DietService(IDietRepository dietRepository, IMapper mapper) {
            _dietRepository = dietRepository;
            _mapper = mapper;
        }

        public async Task<DietDTO> AddDiet(DietDTO diet) {
            if ( diet == null )
                throw new ArgumentNullException(nameof(diet), "Diet cannot be null.");
            try {
                var dietEntity = _mapper.Map<Diet>(diet);

                var addedDiet = await _dietRepository.AddDiet(dietEntity);

                return _mapper.Map<DietDTO>(addedDiet);
            } catch ( Exception ex ) {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<DietDTO> GetDietById(long id) {
            if ( id <= 0 )
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");
            try {
                var diet = await _dietRepository.GetDietById(id);
                return _mapper.Map<DietDTO>(diet);
            } catch ( Exception ex ) {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<DietDTO>> GetDietsByDay(DateTime date, long idPatient) {
            try {
                var diets = await _dietRepository.GetDietsByDay(date, idPatient);
                return _mapper.Map<IEnumerable<DietDTO>>(diets);
            } catch ( Exception ex ) {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<DietDTO> UpdateDiet(DietDTO diet) {
            try {
                var dietEntity = _mapper.Map<Diet>(diet);

                var updatedDiet = await _dietRepository.UpdateDiet(dietEntity);

                return _mapper.Map<DietDTO>(updatedDiet);
            } catch ( Exception ex ) {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteDiet(long id) {
            try {
                return await _dietRepository.DeleteDiet(id);
            } catch ( Exception ex ) {
                throw new ApplicationException("Erro ao deletar dieta", ex);
            }
        }

        public async Task<IEnumerable<DietDTO>> GetDietsByPatient(long id, long idNutritionist) {
            try {
                var diets = await _dietRepository.GetDietsByPatient(id, idNutritionist);
                return _mapper.Map<IEnumerable<DietDTO>>(diets);
            } catch ( Exception ex ) {
                throw new ApplicationException("Erro ao buscar dieta do paciente", ex);
            }
        }
    }
}
