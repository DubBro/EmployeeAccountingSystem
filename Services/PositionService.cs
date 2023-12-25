namespace EmployeeAccountingSystem.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<PositionDTO> GetAsync(int id)
        {
            var result = await _positionRepository.GetAsync(id);
            return _mapper.Map<PositionDTO>(result);
        }

        public async Task<IList<PositionDTO>> ListAsync()
        {
            var result = await _positionRepository.ListAsync();
            return result.Select(p => _mapper.Map<PositionDTO>(p)).ToList();
        }
    }
}
