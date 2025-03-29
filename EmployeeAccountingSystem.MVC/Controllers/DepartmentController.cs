using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.BLL.Services.Interfaces;
using EmployeeAccountingSystem.MVC.ViewModels;
using EmployeeAccountingSystem.MVC.ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccountingSystem.MVC.Controllers;

public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(
        IDepartmentService departmentService,
        IEmployeeService employeeService,
        IMapper mapper,
        ILogger<DepartmentController> logger)
    {
        _departmentService = departmentService;
        _employeeService = employeeService;
        _mapper = mapper;
        _logger = logger;
    }

    // TODO: make exception middleware and remove repeatable try...catch
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _departmentService.ListDepartmentsInfoAsync();
            _logger.LogInformation($"Departments (count = {result.Count}) were received");
            return View(result.Select(e => _mapper.Map<DepartmentInfoViewModel>(e)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var department = await _departmentService.GetAsync(id);
            ViewData["Department"] = _mapper.Map<DepartmentViewModel>(department);

            var filter = new EmployeeFilterRequest() { Department = id };
            var result = await _employeeService.ListFilteredAsync(_mapper.Map<EmployeeFilterModel>(filter));

            return View("DepartmentDetails", result.Select(e => _mapper.Map<EmployeeViewModel>(e)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }
}
