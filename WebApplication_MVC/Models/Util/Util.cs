namespace WebApplication_MVC.Models.Util;

using WebApplication_MVC.Models.Service;
public class Util
{
    public static Boolean checkLongUrl(string longUrl)
    {
        //暂时只考虑过滤非http开头的链接
        if (longUrl.StartsWith("http://") || longUrl.StartsWith("https://"))
        {
            return true;
        }
        return false;
    }
}