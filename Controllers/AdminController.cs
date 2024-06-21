using Dapper;
using duoBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace duoBlog.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var posts = connection
            .Query<Blog>("SELECT id, title, createddate , updatedate  FROM posts")
            .ToList();
        
        return View(posts);
    }
    public IActionResult Sil(int id)
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        
        using var connection = new SqlConnection(connectionString);
        var sql = "DELETE FROM posts WHERE id = @Id";
            
        var rowsAffected = connection.Execute(sql, new { Id = id });

        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(Blog model)
    {
        if(!ModelState.IsValid)
        {
            ViewBag.MessageCssClass = "alert-danger";
            ViewBag.Message = "Eksik veya hatalı işlem yaptın.";
            return View("Message");
        }
        model.CreatedDate = DateTime.Now;
        model.UpdateDate = DateTime.Now;
        
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql =
            "INSERT INTO posts (title, summary, detail, createddate, updatedate) VALUES (@Title, @Summary, @Detail, @CreatedDate, @UpdateDate)";

        var data = new
        {
            model.Title,
            model.Summary,
            model.Detail,
            model.CreatedDate,
            model.UpdateDate
        };
        
        var rowsAffected = connection.Execute(sql, data);

        ViewBag.MessageCssClass = "alert-success";
        ViewBag.Message = "Eklendi.";
        return View("Message");
    }
    [HttpGet]
    public IActionResult Duzenle(int id)
    {
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";

        using var connection = new SqlConnection(connectionString);
        var post = connection.QuerySingleOrDefault<Blog>("SELECT * FROM posts WHERE id = @Id", new {Id = id});

        return View(post);
    }

    [HttpPost]
    public IActionResult Duzenle(Blog model)
    {
        var connectionString = 
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        
        using var connection = new SqlConnection(connectionString);
        var sql = "UPDATE posts SET title=@Title, summary=@Summary, detail=@Detail, updatedate=@UpdateDate WHERE id = @Id";

        var param = new {
            Title = model.Title,
            Summary = model.Summary,
            Detail = model.Detail,
            UpdateDate = DateTime.Now,
            Id = model.Id
        };

        var affectedRows = connection.Execute(sql, param);

        ViewBag.Message = "Güncellendi.";
        ViewBag.MessageCssClass = "alert-success";
        return View("Message");
    }

    [HttpGet]
    public IActionResult AddYorum()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult AddYorum(Yorum model)
    {
        model.CreatedTime = DateTime.Now;
        var connectionString =
            "Server=;Initial Catalog=;User Id=;Password=;TrustServerCertificate=True";
        using var connection = new SqlConnection(connectionString);
        var sql =
            "INSERT INTO comments (UserName, Comment, CreatedTime, PostId) VALUES (@username, @comment, @createdtime, @postid)";
        
        var data = new
        {
            model.UserName,
            model.Comment,
            model.CreatedTime,
            model.PostId
        };
        var rowsAffected = connection.Execute(sql, data);
        
        ViewBag.MessageCssClass = "alert-success";
        ViewBag.Message = "Yorumun alındı. Kontrolden sonra yayına alınacaktır.";
        return View("Message");
    }
}