namespace WebApplication_MVC.Models.Service;


public interface UrlService
{
    String getLongUrlByShortUrl(String shortURL);
    String saveUrlMap(String shortURL, String longURL, String originalURL);
    void updateUrlViews(String shortURL);
}