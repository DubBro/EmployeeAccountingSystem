using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.MVC.ViewModels;
using EmployeeAccountingSystem.MVC.ViewModels.Requests;

namespace EmployeeAccountingSystem.MVC.Infrastructure.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeModel, EmployeeViewModel>().ReverseMap();
        CreateMap<EmployeeFilterRequest, EmployeeFilterModel>();
    }
}
