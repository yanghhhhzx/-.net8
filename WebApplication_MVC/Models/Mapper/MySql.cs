using MySql.Data.MySqlClient;

namespace WebApplication_MVC.Models.Mapper;

public class MySql
{
    static String connectionString = "server=192.168.232.128;port=3306;user=root;password=123;database=mydb;";
    public static string GetLongUrl(String shortUrl)
    {
        string longUrl = String.Empty;
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql="SELECT longUrl FROM url_map WHERE shortUrl = @value";
                using (MySqlCommand command = new MySqlCommand(sql.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@value", shortUrl);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            longUrl = reader["longUrl"].ToString() ?? string.Empty;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // 记录异常或抛出
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return longUrl;
    }

    public static void InsertUrlMapping(String shortUrl, String longUrl)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO url_map (shortUrl, longUrl) VALUES (@shortUrl, @longUrl)";
                using (MySqlCommand command = new MySqlCommand(sql.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@shortUrl", shortUrl);
                    command.Parameters.AddWithValue("@longUrl", longUrl);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            // 记录异常或抛出
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        
    }
}