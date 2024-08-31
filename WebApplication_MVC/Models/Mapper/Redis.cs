namespace WebApplication_MVC.Models.Mapper;
using StackExchange.Redis;

public class Redis
{
    private static ConnectionMultiplexer RedisConnection;

    protected static void Application_Start()
    {
        var connectionString = "192.168.232.128:6379"; // Redis连接字符串，根据你的实际情况进行修改
        RedisConnection = ConnectionMultiplexer.Connect(connectionString);
    }

    public static void Set(string key, string value)
    {
        Application_Start();
        var db = RedisConnection.GetDatabase(); // 获取Redis数据库实例
        db.StringSet("key", "value", TimeSpan.FromHours(24)); // 设置键为"key"的键值对的值为"value"，并设置24小时后过期
        RedisConnection.Close();
    }
    
    public static string Get(string key)
    {       
        Application_Start();
        var db = RedisConnection.GetDatabase(); // 获取Redis数据库实例
        var value = db.StringGet(key); // 获取键为"key"的键值对的值
        Console.WriteLine(value); // 输出键为"key"的键值对的值
        RedisConnection.Close();
        return value;
    }

    //延长过期时间
    public static void Expire(string key)
    {
        Application_Start();
        var db = RedisConnection.GetDatabase(); // 获取Redis数据库实例
        db.KeyExpire(key, TimeSpan.FromHours(24)); // 延长键为"key"的键值对的过期时间为24小时
        RedisConnection.Close();
    }
}