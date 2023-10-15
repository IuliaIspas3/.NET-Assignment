namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public User Owner { get; set; }

    public Post(string Title, string Body, User Owner)
    {
        this.Title = Title;
        this.Body = Body;
        this.Owner = Owner;
    }
}