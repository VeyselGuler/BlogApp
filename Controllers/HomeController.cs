using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using duoBlog.Models;
using Microsoft.Data.SqlClient;

namespace duoBlog.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "SELECT * FROM posts ORDER BY updatedate DESC";
        var posts = connection.Query<Blog>(sql).ToList();
        
        return View(posts);
    }

    public IActionResult Post(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql = "SELECT * FROM posts WHERE id = @id";
        var post = connection.QuerySingleOrDefault<Blog>(sql, new {id});
        
        return View(post);
    }
    
}