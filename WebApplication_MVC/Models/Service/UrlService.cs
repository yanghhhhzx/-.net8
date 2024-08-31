namespace WebApplication_MVC.Models.Service;


public interface UrlService
{
    String getLongUrlByShortUrl(String shortURL);
    String createShortUrl(String longURL);
    void updateUrlViews(String shortURL);
}