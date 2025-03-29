using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.MVC.ViewModels;

namespace EmployeeAccountingSystem.MVC.Infrastructure.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentModel, DepartmentViewModel>();
        CreateMap<DepartmentInfoModel, DepartmentInfoViewModel>();
    }
}
