using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.MVC.ViewModels;

namespace EmployeeAccountingSystem.MVC.Infrastructure.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<PositionModel, PositionViewModel>();
    }
}
