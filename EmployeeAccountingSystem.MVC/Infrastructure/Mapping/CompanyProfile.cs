using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.MVC.ViewModels;

namespace EmployeeAccountingSystem.MVC.Infrastructure.Mapping;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyModel, CompanyViewModel>();
    }
}
