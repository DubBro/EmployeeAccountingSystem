using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.BLL.Services.Interfaces;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;

namespace EmployeeAccountingSystem.BLL.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyModel> GetAsync(int id = 1)
    {
        var result = await _companyRepository.GetAsync(id);
        return _mapper.Map<CompanyModel>(result);
    }
}
