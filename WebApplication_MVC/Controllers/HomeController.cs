using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication_MVC.Models;

namespace WebApplication_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;//声明了一个私有只读字段_logger，用于日志记录。

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}