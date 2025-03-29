using System.Text.RegularExpressions;
using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.BLL.Services.Interfaces;
using EmployeeAccountingSystem.DAL.Entities;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;

namespace EmployeeAccountingSystem.BLL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeModel> GetAsync(int id)
    {
        var result = await _employeeRepository.GetAsync(id);
        return _mapper.Map<EmployeeModel>(result);
    }

    public async Task<ICollection<EmployeeModel>> ListAsync()
    {
        var result = await _employeeRepository.ListAsync();
        return result.Select(e => _mapper.Map<EmployeeModel>(e)).ToList();
    }

    public async Task<ICollection<EmployeeModel>> ListFilteredAsync(EmployeeFilterModel filter)
    {
        // TODO: create method HasAnyValue() and place second part of this check there.
        if (filter == null ||
            (string.IsNullOrEmpty(filter.Name) && string.IsNullOrEmpty(filter.Country) && string.IsNullOrEmpty(filter.City) &&
            filter.MinSalary <= 0 && filter.MaxSalary <= 0 && filter.Department <= 0 && filter.Position <= 0))
        {
            return await ListAsync();
        }

        var result = await _employeeRepository.ListFilteredAsync(_mapper.Map<EmployeeFilter>(filter));
        return result.Select(e => _mapper.Map<EmployeeModel>(e)).ToList();
    }

    public async Task<int> AddAsync(EmployeeModel employeeModel)
    {
        ValidateEmployee(employeeModel);
        return await _employeeRepository.AddAsync(_mapper.Map<EmployeeEntity>(employeeModel));
    }

    public async Task<int> UpdateAsync(EmployeeModel employeeModel)
    {
        ValidateEmployee(employeeModel);

        if (employeeModel.Id <= 0)
        {
            throw new ArgumentException(nameof(employeeModel.Id));
        }

        return await _employeeRepository.UpdateAsync(_mapper.Map<EmployeeEntity>(employeeModel));
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }

    // TODO: mb refactor this, and also place into EmployeeModel?
    private static void ValidateEmployee(EmployeeModel employeeModel)
    {
        if (employeeModel == null)
        {
            throw new ArgumentNullException(nameof(employeeModel));
        }

        Regex wordPattern = new Regex("^[a-zA-Z- ]+$");
        Regex phonePattern = new Regex("^[+][0-9]{10,12}$");

        if (string.IsNullOrWhiteSpace(employeeModel.FirstName))
        {
            throw new ArgumentException(nameof(employeeModel.FirstName));
        }
        else
        {
            employeeModel.FirstName = employeeModel.FirstName.Trim();
            if (!wordPattern.IsMatch(employeeModel.FirstName) || employeeModel.FirstName.Length > 50)
            {
                throw new ArgumentException(nameof(employeeModel.FirstName));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeModel.LastName))
        {
            throw new ArgumentException(nameof(employeeModel.LastName));
        }
        else
        {
            employeeModel.LastName = employeeModel.LastName.Trim();
            if (!wordPattern.IsMatch(employeeModel.LastName) || employeeModel.LastName.Length > 50)
            {
                throw new ArgumentException(nameof(employeeModel.LastName));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeModel.MiddleName))
        {
            throw new ArgumentException(nameof(employeeModel.MiddleName));
        }
        else
        {
            employeeModel.MiddleName = employeeModel.MiddleName.Trim();
            if (!wordPattern.IsMatch(employeeModel.MiddleName) || employeeModel.MiddleName.Length > 50)
            {
                throw new ArgumentException(nameof(employeeModel.MiddleName));
            }
        }

        if (employeeModel.BirthDate <= new DateTime(1950, 1, 1) || employeeModel.BirthDate >= new DateTime(2010, 1, 1))
        {
            throw new ArgumentException(nameof(employeeModel.BirthDate));
        }

        if (string.IsNullOrWhiteSpace(employeeModel.Country))
        {
            employeeModel.Country = null;
        }
        else
        {
            employeeModel.Country = employeeModel.Country.Trim();
            if (!wordPattern.IsMatch(employeeModel.Country) || employeeModel.Country.Length > 50)
            {
                throw new ArgumentException(nameof(employeeModel.Country));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeModel.City))
        {
            employeeModel.City = null;
        }
        else
        {
            employeeModel.City = employeeModel.City.Trim();
            if (!wordPattern.IsMatch(employeeModel.City) || employeeModel.City.Length > 50)
            {
                throw new ArgumentException(nameof(employeeModel.City));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeModel.Address))
        {
            employeeModel.Address = null;
        }
        else
        {
            employeeModel.Address = employeeModel.Address.Trim();
            if (employeeModel.Address.Length > 50)
            {
                throw new ArgumentException(nameof(employeeModel.City));
            }
        }

        if (string.IsNullOrWhiteSpace(employeeModel.Phone))
        {
            employeeModel.Phone = null;
        }
        else
        {
            employeeModel.Phone = employeeModel.Phone.Trim();

            if (!employeeModel.Phone.StartsWith('+'))
            {
                employeeModel.Phone = employeeModel.Phone.Insert(0, "+");
            }

            if (!phonePattern.IsMatch(employeeModel.Phone))
            {
                throw new ArgumentException(nameof(employeeModel.Phone));
            }
        }

        if (employeeModel.HireDate <= employeeModel.BirthDate || employeeModel.HireDate <= new DateTime(1960, 1, 1) || employeeModel.HireDate > DateTime.Now)
        {
            throw new ArgumentException(nameof(employeeModel.HireDate));
        }

        if (employeeModel.Salary <= 0)
        {
            throw new ArgumentException(nameof(employeeModel.Salary));
        }

        if (employeeModel.DepartmentId <= 0)
        {
            throw new ArgumentException(nameof(employeeModel.DepartmentId));
        }

        if (employeeModel.PositionId <= 0)
        {
            throw new ArgumentException(nameof(employeeModel.PositionId));
        }
    }
}
