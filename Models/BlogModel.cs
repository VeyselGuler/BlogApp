namespace duoBlog.Models;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Detail { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdateDate { get; set; }
}

public class Yorum
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedTime { get; set; }
    public int PostId { get; set; }
    public Boolean IsApproved { get; set; }
    
}