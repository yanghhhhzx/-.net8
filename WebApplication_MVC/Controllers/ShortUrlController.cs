namespace WebApplication_MVC.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models.Service;
using Models.Util;

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

    [HttpPost]//不用get是因为把一个长链作为参数放url不好，太长了
    public string CreateShortUrl(HttpContext context)
    {
        string longUrl = context.Request.Form["longUrl"].ToString();
        // 读取请求体中的数据
        if (Util.checkLongUrl(longUrl)==false)
        {
            return "错误的长链接格式";
        }
        string shortUrl = _urlService.createShortUrl(longUrl);
        return shortUrl;
    }
    
}