namespace EmployeeAccountingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ICompanyService companyService, IMapper mapper, ILogger<HomeController> logger)
    {
        _companyService = companyService;
        _mapper = mapper;
        _logger = logger;
    }

    // TODO: make exception middleware and remove repeatable try...catch
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _companyService.GetAsync();
            _logger.LogInformation($"Company '{result.Name}' with id = {result.Id} was received");
            return View(_mapper.Map<CompanyViewModel>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return View("Error");
        }
    }
}
