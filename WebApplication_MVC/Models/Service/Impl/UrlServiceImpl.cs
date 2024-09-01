namespace WebApplication_MVC.Models.Service.Impl;

using Models.Mapper;
using System.Data.HashFunction.MurmurHash;
using System;
using System.Text;
public class UrlServiceImpl : UrlService
{
    public  String getLongUrlByShortUrl(String shortUrl)
    {
        if (Redis.IsInBloom(shortUrl))
        {
            string longUrl = Redis.Get(shortUrl); //尝试从缓存中获取长链接
            if (string.IsNullOrEmpty(longUrl)) //如果缓存中没有
            {
                longUrl = MySql.GetLongUrl(shortUrl); //尝试从数据库中获取长链接
                if (!string.IsNullOrEmpty(longUrl))
                {
                    Redis.Set(shortUrl, longUrl); //缓存长链接
                }
            }
            else
            {
                //如果有缓存,延长过期时间
                Redis.Expire(shortUrl);
            }
            return longUrl;
        }
        return string.Empty;
    }

    public String createShortUrl(String longURL)
    {
        // 1. 利用MurmurHash3算法，生成生成一个短链接
        byte[] lBytes= Encoding.UTF8.GetBytes(longURL);
        IMurmurHash3 murmurHash = MurmurHash3Factory.Instance.Create(new MurmurHash3Config() { HashSizeInBits = 32,  Seed = 0 });
        //HashSizeInBits=32时, Base64的Hash值长度只有6位
        string shortUrl = murmurHash.ComputeHash(lBytes).AsBase64String();
        //布隆过滤器初步检查
        if (Redis.IsInBloom(shortUrl))
        {
            // 检查是否有相同的短链接，如果有，则重新生成
            if (!string.IsNullOrEmpty(getLongUrlByShortUrl(shortUrl)))
            {
                //如果有相同的短链接，则随便再加一个字符，重新生成
                //这种做法再短链接数量非常多时，性能会持续下降，最终导致永远递归，
                //但是先暂时这样，毕竟对于短链接的最优生成算法，争议还是挺多的
                shortUrl=createShortUrl(longURL+"a");
            }
        }
        // 2. 保存短链接与长链接的映射关系
        MySql.InsertUrlMapping(shortUrl, longURL);

        // 3. 缓存短链接与长链接的映射关系
        Redis.Set(shortUrl, longURL);
        Redis.AddIntoBloom(shortUrl);
        return shortUrl;
    }
    public void updateUrlViews(String shortURL)
    {
        Redis.Expire(shortURL);
    }
}