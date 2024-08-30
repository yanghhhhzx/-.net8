namespace WebApplication_MVC.Models.Service.Impl;

using Models.Mapper;
public class UrlServiceImpl : UrlService
{
    public String getLongUrlByShortUrl(String shortUrl)
    {
        return MySql.GetLongUrl(shortUrl);
    }

    public String saveUrlMap(String shortURL, String longURL, String originalURL)
    {
        return "";
    }


    public void updateUrlViews(String shortURL)
    {
    }
}