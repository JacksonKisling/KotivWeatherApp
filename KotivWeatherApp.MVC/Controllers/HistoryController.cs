using Business.Services;
using Microsoft.AspNetCore.Mvc;

public class HistoryController : Controller
{
    private readonly SearchHistoryService _service;

    public HistoryController(SearchHistoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> LastFive()
    {
        var results = await _service.GetLastFiveAsync();
        return PartialView("~/Views/History/_LastFivePartial.cshtml", results);
    }

    [HttpGet]
    public async Task<IActionResult> LastFiveJson()
    {
        var list = await _service.GetLastFiveAsync();
        return Json(list);
    }

}
