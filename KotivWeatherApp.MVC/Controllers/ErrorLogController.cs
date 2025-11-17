using Business.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KotivWeatherApp.MVC.Controllers
{
    public class ErrorLogController : Controller
    {
        private readonly IErrorLogRepository _repo;

        public ErrorLogController(IErrorLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _repo.GetAllAsync();
            return View(logs);
        }
    }
}
