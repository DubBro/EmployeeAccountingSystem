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

            CreateMap<CompanyDTO, CompanyViewModel>();

            CreateMap<EmployeeFilterDTO, EmployeeFilter>();
            CreateMap<EmployeeFilterRequest, EmployeeFilterDTO>();
        }
    }
}
