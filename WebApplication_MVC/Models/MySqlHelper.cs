namespace WebApplication_MVC.Models;

using MySql.Data.MySqlClient;
using System.Data;

public class MySqlHelper
{
    static String connectionString = "server=192.168.232.128;port=3306;user=root;password=123;database=mydb;";
    private MySqlConnection conn = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader sdr;
    private MySqlDataAdapter sda = null;

    public MySqlHelper()
    {
        conn = new MySqlConnection(connectionString); //数据库连接
    }

    //开启数据库连接
    private MySqlConnection GetConn()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        return conn;
    }

    //  关闭数据库链接
    private void GetConnClose()
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }

    /// <summary>
    /// 执行不带参数的增删改SQL语句或存储过程
    /// </summary>
    /// <param name="cmdText">增删改SQL语句或存储过程的字符串</param>
    /// <param name="ct">命令类型</param>
    /// <returns>受影响的函数</returns>
    public int ExecuteNonQuery(string cmdText, CommandType ct)
    {
        int res;
        using (cmd = new MySqlCommand(cmdText, GetConn()))
        {
            cmd.CommandType = ct;
            res = cmd.ExecuteNonQuery();
        }
        return res;
    }

    /// <summary>
    /// 执行带参数的增删改SQL语句或存储过程
    /// </summary>
    /// <param name="cmdText">增删改SQL语句或存储过程的字符串</param>
    /// <param name="paras">往存储过程或SQL中赋的参数集合</param>
    /// <param name="ct">命令类型</param>
    /// <returns>受影响的函数</returns>
    public int ExecuteNonQuery(string cmdText, MySqlParameter[] paras, CommandType ct)
    {
        int res;
        using (cmd = new MySqlCommand(cmdText, GetConn()))
        {
            cmd.CommandType = ct;
            cmd.Parameters.AddRange(paras);
            res = cmd.ExecuteNonQuery();
        }

        return res;
    }

    /// <summary>
    /// 执行不带参数的查询SQL语句或存储过程
    /// </summary>
    /// <param name="cmdText">查询SQL语句或存储过程的字符串</param>
    /// <param name="ct">命令类型</param>
    /// <returns>查询到的DataTable对象</returns>
    public DataTable ExecuteQuery(string cmdText, CommandType ct)
    {
        DataTable dt = new DataTable();
        cmd = new MySqlCommand(cmdText, GetConn());
        cmd.CommandType = ct;
        using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        {
            dt.Load(sdr);
        }

        return dt;
    }

    /// <summary>
    /// 执行带参数的查询SQL语句或存储过程
    /// </summary>
    /// <param name="cmdText">查询SQL语句或存储过程的字符串</param>
    /// <param name="paras">参数集合</param>
    /// <param name="ct">命令类型</param>
    /// <returns></returns>
    public DataTable ExecuteQuery(string cmdText, MySqlParameter[] paras, CommandType ct)
    {
        DataTable dt = new DataTable();
        cmd = new MySqlCommand(cmdText, GetConn());
        cmd.CommandType = ct;
        cmd.Parameters.AddRange(paras);
        using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        {
            dt.Load(sdr);
        }

        return dt;
    }

    /// <summary>
    /// 执行指定数据库连接字符串的命令,返回DataSet.
    /// </summary>
    /// <param name="strSql">一个有效的数据库连接字符串</param>
    /// <returns>返回一个包含结果集的DataSet</returns>
    public DataSet ExecuteDataset(string strSql)
    {
        DataSet ds = new DataSet();
        sda = new MySqlDataAdapter(strSql, GetConn());
        try
        {
            sda.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            GetConnClose();
        }
        return ds;
    }

    static void SQLQuery(string sql)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: {0}, Name: {1}", reader["id"], reader["name"]);
                    }
                }
            }

            connection.Close();
        }
    }

    public static void GetUsers(String user)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM your_table WHERE column1 = @value";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@value", user);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader["column1"], reader["column2"]);
                    }
                }
            }
        }
    }
}