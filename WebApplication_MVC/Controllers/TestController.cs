using Microsoft.AspNetCore.Mvc;
using WebApplication_MVC.Models;
namespace WebApplication_MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController
{
    [HttpGet]
    public string Get()
    {
        return "Hello World! I am a test controller.";
    }
    
}