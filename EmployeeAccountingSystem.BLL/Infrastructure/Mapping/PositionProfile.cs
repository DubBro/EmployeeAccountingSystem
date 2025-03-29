using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.BLL.Infrastructure.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<PositionEntity, PositionModel>();
    }
}
