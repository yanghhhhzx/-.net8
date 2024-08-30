namespace WebApplication_MVC.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data;


//请求http://localhost:5281/HelloWorld/会被路由到这里。controller会默认被去掉
public class HelloWorldController : Controller
{

    // GET: /HelloWorld/
    //假设不接请求路径参数，则默认调用Index方法
    public IActionResult Index()
    {
        //之前返回不成功，是因为hello word多了一个w。
        return View();
    }

    //路径参数可以直接写在方法参数中，如：
    // GET: http://localhost:5281/HelloWorld/Welcome/3?name=Rick
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }

    public string getUsers()
    {
        MySqlHelper h = new MySqlHelper();
        string sql = "select * from users";
        DataTable ds = h.ExecuteQuery(sql,CommandType.Text);
        return ds.Rows.Count.ToString();
    }
}