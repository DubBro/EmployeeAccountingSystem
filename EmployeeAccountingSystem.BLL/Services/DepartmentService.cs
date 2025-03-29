using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.BLL.Services.Interfaces;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;

namespace EmployeeAccountingSystem.BLL.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<DepartmentModel> GetAsync(int id)
    {
        var result = await _departmentRepository.GetAsync(id);
        return _mapper.Map<DepartmentModel>(result);
    }

    public async Task<ICollection<DepartmentModel>> ListAsync()
    {
        var result = await _departmentRepository.ListAsync();
        return result.Select(d => _mapper.Map<DepartmentModel>(d)).ToList();
    }

    public async Task<ICollection<DepartmentInfoModel>> ListDepartmentsInfoAsync()
    {
        var result = await _departmentRepository.ListDepartmentsInfoAsync();
        return result.Select(d => _mapper.Map<DepartmentInfoModel>(d)).ToList();
    }
}
