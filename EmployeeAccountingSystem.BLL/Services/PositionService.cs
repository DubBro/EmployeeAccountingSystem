using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.BLL.Services.Interfaces;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;

namespace EmployeeAccountingSystem.BLL.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public PositionService(IPositionRepository positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionModel> GetAsync(int id)
    {
        var result = await _positionRepository.GetAsync(id);
        return _mapper.Map<PositionModel>(result);
    }

    public async Task<ICollection<PositionModel>> ListAsync()
    {
        var result = await _positionRepository.ListAsync();
        return result.Select(p => _mapper.Map<PositionModel>(p)).ToList();
    }
}
