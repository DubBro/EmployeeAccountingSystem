namespace EmployeeAccountingSystem.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentDTO> GetAsync(int id)
        {
            var result = await _departmentRepository.GetAsync(id);
            return _mapper.Map<DepartmentDTO>(result);
        }

        public async Task<IList<DepartmentDTO>> ListAsync()
        {
            var result = await _departmentRepository.ListAsync();
            return result.Select(d => _mapper.Map<DepartmentDTO>(d)).ToList();
        }
    }
}
