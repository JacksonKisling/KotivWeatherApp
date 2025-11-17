using Business.Repositories;
using Microsoft.AspNetCore.Mvc;

public class ApiLogController : Controller
{
    private readonly IApiLogRepository _repository;

    public ApiLogController(IApiLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var logs = await _repository.GetAllAsync();
        return View(logs);
    }

}
