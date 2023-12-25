namespace EmployeeAccountingSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyEntity, CompanyDTO>();
            CreateMap<DepartmentEntity, DepartmentDTO>();
            CreateMap<PositionEntity, PositionDTO>();
            CreateMap<EmployeeEntity, EmployeeDTO>().ReverseMap();
            CreateMap<DepartmentInfo, DepartmentInfoDTO>();

            CreateMap<CompanyDTO, CompanyViewModel>();
            CreateMap<DepartmentDTO, DepartmentViewModel>();
            CreateMap<PositionDTO, PositionViewModel>();
            CreateMap<EmployeeDTO, EmployeeViewModel>().ReverseMap();
            CreateMap<DepartmentInfoDTO, DepartmentInfoViewModel>();

            CreateMap<EmployeeFilterDTO, EmployeeFilter>();
            CreateMap<EmployeeFilterRequest, EmployeeFilterDTO>();
        }
    }
}
