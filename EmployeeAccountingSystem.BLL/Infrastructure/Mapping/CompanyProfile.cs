using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.BLL.Infrastructure.Mapping;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyEntity, CompanyModel>();
    }
}
