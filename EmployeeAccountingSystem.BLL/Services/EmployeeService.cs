using System.Text.RegularExpressions;

namespace EmployeeAccountingSystem.Services;

// TODO: mb rename all DTOs into Models?
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeDTO> GetAsync(int id)
    {
        var result = await _employeeRepository.GetAsync(id);
        return _mapper.Map<EmployeeDTO>(result);
    }

    public async Task<IList<EmployeeDTO>> ListAsync()
    {
        var result = await _employeeRepository.ListAsync();
        return result.Select(e => _mapper.Map<EmployeeDTO>(e)).ToList();
    }

    public async Task<IList<EmployeeDTO>> ListFilteredAsync(EmployeeFilterDTO filter)
    {
        // TODO: create in EmployeeFilterDTO method HasAnyValue() and place second part of this check there.
        // After that EmployeeFilterDTO should be renamed to EmployeeFilterModel
        if (filter == null ||
            (string.IsNullOrEmpty(filter.Name) && string.IsNullOrEmpty(filter.Country) && string.IsNullOrEmpty(filter.City) &&
            filter.MinSalary <= 0 && filter.MaxSalary <= 0 && filter.Department <= 0 && filter.Position <= 0))
        {
            return await ListAsync();
        }

        var result = await _employeeRepository.ListFilteredAsync(_mapper.Map<EmployeeFilter>(filter));
        return result.Select(e => _mapper.Map<EmployeeDTO>(e)).ToList();
    }

    public async Task<int> AddAsync(EmployeeDTO employeeDTO)
    {
        ValidateEmployee(employeeDTO);
        return await _employeeRepository.AddAsync(_mapper.Map<EmployeeEntity>(employeeDTO));
    }

    public async Task<int> UpdateAsync(EmployeeDTO employeeDTO)
    {
        ValidateEmployee(employeeDTO);

        if (employeeDTO.Id <= 0)
        {
            throw new ArgumentException(nameof(employeeDTO.Id));
        }

        return await _employeeRepository.UpdateAsync(_mapper.Map<EmployeeEntity>(employeeDTO));
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }

    // TODO: mb refactor this, and also place into EmployeeDTO?
    private static void ValidateEmployee(EmployeeDTO employeeDTO)
    {
        if (employeeDTO == null)
        {
            throw new ArgumentNullException(nameof(employeeDTO));
        }

        Regex wordPattern = new Regex("^[a-zA-Z- ]+$");
        Regex phonePattern = new Regex("^[+][0-9]{10,12}$");

        if (string.IsNullOrWhiteSpace(employeeDTO.FirstName))
        {
            throw new ArgumentException(nameof(employeeDTO.FirstName));
        }
        else
        {
            employeeDTO.FirstName = employeeDTO.FirstName.Trim();
            if (!wordPattern.IsMatch(employeeDTO.FirstName) || employeeDTO.FirstName.Length > 50)
            {
                throw new ArgumentException(nameof(employeeDTO.FirstName));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.LastName))
        {
            throw new ArgumentException(nameof(employeeDTO.LastName));
        }
        else
        {
            employeeDTO.LastName = employeeDTO.LastName.Trim();
            if (!wordPattern.IsMatch(employeeDTO.LastName) || employeeDTO.LastName.Length > 50)
            {
                throw new ArgumentException(nameof(employeeDTO.LastName));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.MiddleName))
        {
            throw new ArgumentException(nameof(employeeDTO.MiddleName));
        }
        else
        {
            employeeDTO.MiddleName = employeeDTO.MiddleName.Trim();
            if (!wordPattern.IsMatch(employeeDTO.MiddleName) || employeeDTO.MiddleName.Length > 50)
            {
                throw new ArgumentException(nameof(employeeDTO.MiddleName));
            }
        }

        if (employeeDTO.BirthDate <= new DateTime(1950, 1, 1) || employeeDTO.BirthDate >= new DateTime(2010, 1, 1))
        {
            throw new ArgumentException(nameof(employeeDTO.BirthDate));
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.Country))
        {
            employeeDTO.Country = null;
        }
        else
        {
            employeeDTO.Country = employeeDTO.Country.Trim();
            if (!wordPattern.IsMatch(employeeDTO.Country) || employeeDTO.Country.Length > 50)
            {
                throw new ArgumentException(nameof(employeeDTO.Country));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.City))
        {
            employeeDTO.City = null;
        }
        else
        {
            employeeDTO.City = employeeDTO.City.Trim();
            if (!wordPattern.IsMatch(employeeDTO.City) || employeeDTO.City.Length > 50)
            {
                throw new ArgumentException(nameof(employeeDTO.City));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.Address))
        {
            employeeDTO.Address = null;
        }
        else
        {
            employeeDTO.Address = employeeDTO.Address.Trim();
            if (employeeDTO.Address.Length > 50)
            {
                throw new ArgumentException(nameof(employeeDTO.City));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.Phone))
        {
            employeeDTO.Phone = null;
        }
        else
        {
            employeeDTO.Phone = employeeDTO.Phone.Trim();

            if (!employeeDTO.Phone.StartsWith('+'))
            {
                employeeDTO.Phone = employeeDTO.Phone.Insert(0, "+");
            }

            if (!phonePattern.IsMatch(employeeDTO.Phone))
            {
                throw new ArgumentException(nameof(employeeDTO.Phone));
            }
        }

        if (employeeDTO.HireDate <= employeeDTO.BirthDate || employeeDTO.HireDate <= new DateTime(1960, 1, 1) || employeeDTO.HireDate > DateTime.Now)
        {
            throw new ArgumentException(nameof(employeeDTO.HireDate));
        }

        if (employeeDTO.Salary <= 0)
        {
            throw new ArgumentException(nameof(employeeDTO.Salary));
        }

        if (employeeDTO.DepartmentId <= 0)
        {
            throw new ArgumentException(nameof(employeeDTO.DepartmentId));
        }

        if (employeeDTO.PositionId <= 0)
        {
            throw new ArgumentException(nameof(employeeDTO.PositionId));
        }
    }
}
