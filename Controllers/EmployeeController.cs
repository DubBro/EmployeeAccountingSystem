namespace EmployeeAccountingSystem.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly IPositionService _positionService;
    private readonly IMapper _mapper;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(
        IEmployeeService employeeService,
        IDepartmentService departmentService,
        IPositionService positionService,
        IMapper mapper,
        ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        _positionService = positionService;
        _mapper = mapper;
        _logger = logger;
    }

    // TODO: make exception middleware and remove repeatable try...catch
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var departments = await _departmentService.ListAsync();
            var positions = await _positionService.ListAsync();
            ViewData["Departments"] = departments.Select(d => _mapper.Map<DepartmentViewModel>(d)).ToList();
            ViewData["Positions"] = positions.Select(p => _mapper.Map<PositionViewModel>(p)).ToList();
            ViewData["CurrentFilter"] = new EmployeeFilterRequest();

            var result = await _employeeService.ListAsync();
            _logger.LogInformation($"Employees (count = {result.Count}) were received");
            return View(result.Select(e => _mapper.Map<EmployeeViewModel>(e)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Index(EmployeeFilterRequest filter)
    {
        try
        {
            var departments = await _departmentService.ListAsync();
            var positions = await _positionService.ListAsync();
            ViewData["Departments"] = departments.Select(d => _mapper.Map<DepartmentViewModel>(d)).ToList();
            ViewData["Positions"] = positions.Select(p => _mapper.Map<PositionViewModel>(p)).ToList();
            ViewData["CurrentFilter"] = filter;

            var result = await _employeeService.ListFilteredAsync(_mapper.Map<EmployeeFilterDTO>(filter));
            _logger.LogInformation($"Employees (count = {result.Count}) were received");
            return View(result.Select(e => _mapper.Map<EmployeeViewModel>(e)).ToList());
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
            var departments = await _departmentService.ListAsync();
            var positions = await _positionService.ListAsync();
            ViewData["Departments"] = departments.Select(d => _mapper.Map<DepartmentViewModel>(d)).ToList();
            ViewData["Positions"] = positions.Select(p => _mapper.Map<PositionViewModel>(p)).ToList();

            var result = await _employeeService.GetAsync(id);
            _logger.LogInformation($"Employee with id = {result.Id} was received");
            return View("EmployeeCard", _mapper.Map<EmployeeViewModel>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        try
        {
            var departments = await _departmentService.ListAsync();
            var positions = await _positionService.ListAsync();
            ViewData["Departments"] = departments.Select(d => _mapper.Map<DepartmentViewModel>(d)).ToList();
            ViewData["Positions"] = positions.Select(p => _mapper.Map<PositionViewModel>(p)).ToList();

            return View("AddEmployeeForm");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(EmployeeViewModel employee)
    {
        try
        {
            var result = await _employeeService.AddAsync(_mapper.Map<EmployeeModel>(employee));
            _logger.LogInformation($"Employee with id = {result} was added");
            return View("AddEmployeeSuccess");
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("InvalidDataError");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeViewModel employee)
    {
        try
        {
            var result = await _employeeService.UpdateAsync(_mapper.Map<EmployeeModel>(employee));
            _logger.LogInformation($"Employee with id = {result} was updated");
            return View("UpdateEmployeeSuccess");
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("InvalidDataError");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _employeeService.DeleteAsync(id);
            _logger.LogInformation($"Employee with id = {result} was deleted");
            return View("DeleteEmployeeSuccess");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }
}
