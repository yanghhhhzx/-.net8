namespace WebApplication_MVC.Models.Mapper;
using StackExchange.Redis;

public class Redis
{
    private static ConnectionMultiplexer RedisConnection;
    private static IDatabase db;
    private const string BloomFilterKey = "ShortUrlBloomFilter";  

    protected static void Application_Start()
    {
        var connectionString = "192.168.232.128:6379"; // Redis连接字符串，根据你的实际情况进行修改
        RedisConnection = ConnectionMultiplexer.Connect(connectionString);
        db = RedisConnection.GetDatabase();
    }

    public static void Set(string key, string value)
    {
        Application_Start();
        db.StringSet("key", "value", TimeSpan.FromHours(24)); // 设置键为"key"的键值对的值为"value"，并设置24小时后过期
        RedisConnection.Close();
    }
    
    public static string Get(string key)
    {       
        Application_Start();
        var value = db.StringGet(key); // 获取键为"key"的键值对的值
        Console.WriteLine(value); // 输出键为"key"的键值对的值
        RedisConnection.Close();
        return value;
    }

    //延长过期时间
    public static void Expire(string key)
    {
        Application_Start();
        db.KeyExpire(key, TimeSpan.FromHours(24)); // 延长键为"key"的键值对的过期时间为24小时
        RedisConnection.Close();
    }

    public static void AddIntoBloom(string value)
    {
        Application_Start();
        var result = db.Execute("BF.ADD", BloomFilterKey, value); //利用Execute命令直接执行需要的语句,就不用记太多方法
        Console.WriteLine(result); // 输出执行结果
        RedisConnection.Close();
    }

    public static bool IsInBloom(string value)
    {
        Application_Start();
        RedisResult result = db.Execute("BF.EXISTS", BloomFilterKey, value); //利用Execute命令直接执行需要的语句,就不用记太多方法
        Console.WriteLine(result); // 输出执行结果
        RedisConnection.Close();
        return result.ToString().Contains("True");
        // 注意: BF.EXISTS命令返回的是一个布尔值,所以这里需要判断返回值是否包含"True"字符串
    }
}