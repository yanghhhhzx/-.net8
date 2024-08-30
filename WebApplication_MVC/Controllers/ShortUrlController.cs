namespace WebApplication_MVC.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models.Service;
using System.Data;
public class ShortUrlController  : Controller
{
    // readonly是只读
    private readonly UrlService _urlService;
    // 利用构造函数，会自动进行依赖注入，不需要去调用
    public ShortUrlController(UrlService urlService)
    {
        _urlService = urlService;
    }
    
    [HttpGet]
    public string Get(String shortUrl)
    {
        String longUrl = _urlService.getLongUrlByShortUrl(shortUrl);
        if (!string.IsNullOrEmpty(longUrl)) {
            _urlService.updateUrlViews(shortUrl);
            //查询到对应的原始链接，302重定向
            return "redirect:" + longUrl;
        }
        //没有对应的原始链接，直接返回首页
        return "redirect:/";
    }

    [HttpPost]
    public IActionResult Create(string longUrl)
    {
        // Generate a unique short URL
        string shortUrl = GenerateShortUrl();

        // Save the long URL and short URL to the database
        SaveUrlToDatabase(longUrl, shortUrl);

        // Redirect the user to the short URL
        return Redirect(shortUrl);
    }

    private string GenerateShortUrl()
    {
        // Generate a unique short URL
        // TODO: Implement a unique short URL generation algorithm
        return "abc123";
    }

    private void SaveUrlToDatabase(string longUrl, string shortUrl)
    {
        // Save the long URL and short URL to the database
        // TODO: Implement database connection and save the data
    }
}