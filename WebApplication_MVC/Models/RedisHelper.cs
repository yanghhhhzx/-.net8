namespace WebApplication_MVC.Models;

using StackExchange.Redis;


public class RedisHelper
{
    private static ConnectionMultiplexer RedisConnection;

    protected void Application_Start()
    {
        var connectionString = "192.168.232.128:6379"; // Redis连接字符串，根据你的实际情况进行修改
        RedisConnection = ConnectionMultiplexer.Connect(connectionString);
    }

    public void Set()
    {
        var db = RedisConnection.GetDatabase(); // 获取Redis数据库实例
        db.StringSet("key", "value"); // 设置键为"key"，值为"value"的键值对
    }
    
    public void Get(string key)
    {
        var db = RedisConnection.GetDatabase(); // 获取Redis数据库实例
        var value = db.StringGet(key); // 获取键为"key"的键值对的值
        Console.WriteLine(value); // 输出键为"key"的键值对的值
    }
}